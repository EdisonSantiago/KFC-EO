using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Configuration;
using Oulanka.Configuration.Models;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;

namespace Oulanka.Services
{
    public class JobsService : IDisposable, IJobsService
    {
        private static readonly JobsService _jobsService;
        private readonly DateTime _created;
        private readonly IConfigurationSettings _configuration;
        private DateTime _completed;
        private int _interval = 15 * 6000;
        private bool _isRunning;
        private Timer _singleTimer;
        private DateTime _started;

        public Hashtable CurrentJobs { get; }

        public ListDictionary CurrentStats => new ListDictionary
        {
            { "Created", _created },
            { "LastStart", _started },
            { "LastStop", _completed },
            { "IsRunning", _isRunning },
            { "Minutes", _interval / 60000 }
        };

        static JobsService()
        {
            _jobsService = new JobsService();
        }

        private JobsService()
        {
            CurrentJobs = new Hashtable();
            _configuration = ServiceLocator.Current.GetInstance<IConfigurationSettings>();
            _created = DateTime.Now;
        }

        public static JobsService Instance()
        {
            return _jobsService;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _singleTimer.Dispose();
            }
        }

        public void Callback(object state)
        {
            _isRunning = true;
            _started = DateTime.Now;
            _singleTimer.Change(Timeout.Infinite, Timeout.Infinite);

            foreach (var job in CurrentJobs.Values.Cast<Job>().Where(job => job.Enabled && job.SingleThreaded))
            {
                job.Execute();
            }

            _singleTimer.Change(_interval, _interval);
            _isRunning = false;
            _completed = DateTime.Now;
        }

        public bool IsJobEnabled(string jobName)
        {
            return CurrentJobs.Contains(jobName) && ((Job)CurrentJobs[jobName]).Enabled;
        }

        public void Start()
        {
            if (CurrentJobs.Count != 0)
            {
                return;
            }

            var configuration = _configuration.GetConfig();

            if (!configuration.DisableBackgroundThreads)
            {
                var jobs = configuration.Jobs.Items;

                foreach (JobItemConfigurationElement job in jobs)
                {
                    var type = Type.GetType(job.Type);
                    if (type != null)
                    {
                        if (!CurrentJobs.Contains(job.Name))
                        {
                            var newJob = new Job(type, job);
                            CurrentJobs[job.Name] = newJob;

                            if (!configuration.Jobs.SingleThread || !job.SingleThread)
                            {
                                newJob.InitializeTimer();
                            }
                        }
                    }
                }

                if (configuration.Jobs.SingleThread)
                {
                    try
                    {
                        _interval = configuration.Jobs.Minutes * 60000;
                    }
                    catch
                    {
                        _interval = 15 * 60000;
                    }

                    _singleTimer = new Timer(Callback, null, _interval, _interval);
                }
            }
        }

        public void Stop()
        {
            if (CurrentJobs == null)
            {
                return;
            }

            foreach (Job job in CurrentJobs.Values)
            {
                job.Dispose();
            }

            CurrentJobs.Clear();

            if (_singleTimer != null)
            {
                _singleTimer.Dispose();
                _singleTimer = null;
            }
        }

        public override string ToString()
        {
            return
                $"Created: {_created}, LastStart: {_started}, LastStop: {_completed}, IsRunning: {_isRunning}, Minutes: {_interval/60000}";
        }
    }
}