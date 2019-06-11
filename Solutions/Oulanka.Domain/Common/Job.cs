using System;
using System.Threading;
using System.Xml.Serialization;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Configuration.Models;

namespace Oulanka.Domain.Common
{
    public class Job : IDisposable
    {
        public EventHandler PostJob;
        public EventHandler PreJob;
        [NonSerialized]
        private readonly JobItemConfigurationElement _jobElement;
        private int _firstRun = -1;
        private IJob _iJob;
        private int _seconds = -1;
        [NonSerialized]
        private Timer _timer;
        private bool _wasDisposed;


        public Job(Type job, JobItemConfigurationElement jobElement)
        {
            _jobElement = jobElement;
            JobType = job;
            Enabled = jobElement.Enabled;
            Name = jobElement.Name;
            Minutes = jobElement.Minutes;
            EnableShutDown = jobElement.EnableShutDown;
            SingleThreaded = jobElement.SingleThread;
        }


        public bool EnableShutDown { get; }
        public bool Enabled { get; private set; }
        public bool IsRunning { get; private set; }
        [XmlIgnore]
        public Type JobType { get; }
        public DateTime LastEnd { get; private set; }
        public DateTime LastStarted { get; private set; }
        public DateTime LastSuccess { get; private set; }
        public int Minutes { get; set; }
        public string Name { get; private set; }
        public bool SingleThreaded { get; private set; }
        protected int Interval
        {
            get
            {
                if (_firstRun > 0) return _firstRun*1000;

                if (_seconds > 0) return _seconds*1000;

                return Minutes * 60000;
            }
        }


        public IJob CreateInstance()
        {
            if (Enabled)
            {
                if (_iJob == null)
                {
                    if (JobType != null)
                    {
                        try
                        {
                            object current = ServiceLocator.Current.GetInstance(JobType);
                            _iJob = current as IJob;
                        }
                        catch (Exception)
                        {
                            Dispose();
                        }
                    }

                    Enabled = _iJob != null;
                    if (!Enabled)
                    {
                        Dispose();
                    }
                }
            }

            return _iJob;
        }


        public void Execute()
        {
            OnPreJob();

            IsRunning = true;
            IJob ijob = CreateInstance();


            if (ijob != null)
            {
                LastStarted = DateTime.Now;
                try
                {
                    ijob.Execute(_jobElement);
                    LastEnd = LastSuccess = DateTime.Now;
                }
                catch (Exception)
                {
                    Enabled = !EnableShutDown;
                    LastEnd = DateTime.Now;
                }
            }

            IsRunning = false;
            OnPostJob();
        }

        public void InitializeTimer()
        {
            if (_timer == null && Enabled)
            {
                _timer = new Timer(OnTimerCallback, null, Interval, Interval);
            }
        }

        public void Dispose()
        {
            if (_timer != null && !_wasDisposed)
            {
                lock (this)
                {
                    _timer.Dispose();
                    _timer = null;
                    _wasDisposed = true;
                }
            }
        }


        private void OnPostJob()
        {
            try
            {
                if (PostJob != null)
                {
                    PostJob(this, EventArgs.Empty);
                }
            }
            catch (Exception)
            {
                // do something with the exceptions;
            }
        }

        private void OnPreJob()
        {
            try
            {
                if (PreJob != null)
                {
                    PreJob(this, EventArgs.Empty);
                }
            }
            catch (Exception)
            {
                // do something with the exceptions;
            }
        }

        private void OnTimerCallback(object state)
        {
            if (!Enabled)
            {
                return;
            }

            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _firstRun = -1;

            Execute();

            if (Enabled)
            {
                _timer.Change(Interval, Interval);
            }
            else
            {
                Dispose();
            }
        }
    }
}