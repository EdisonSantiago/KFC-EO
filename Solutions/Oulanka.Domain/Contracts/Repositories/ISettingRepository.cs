using Oulanka.Domain.Models;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface ISettingRepository : IRepository<Setting>
    {
        Setting Get(string option, string name);
    }
}