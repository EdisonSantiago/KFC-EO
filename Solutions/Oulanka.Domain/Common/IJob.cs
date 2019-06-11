using Oulanka.Configuration.Models;

namespace Oulanka.Domain.Common
{
    public interface IJob
    {
        void Execute(JobItemConfigurationElement jobElement);
    }
}