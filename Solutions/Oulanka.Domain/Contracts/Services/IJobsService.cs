using System.Collections;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IJobsService
    {
        Hashtable CurrentJobs { get; }

        void Callback(object state);
        bool IsJobEnabled(string jobName);
        void Start();
        void Stop();
        string ToString();

    }
}