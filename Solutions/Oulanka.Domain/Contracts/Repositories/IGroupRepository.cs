using Oulanka.Domain.Models;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IGroupRepository : IRepository<Grupo>
    {
        Grupo GetByName(string groupName);
    }
}