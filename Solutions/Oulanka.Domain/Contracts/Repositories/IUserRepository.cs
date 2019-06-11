using Oulanka.Domain.Common;
using Oulanka.Domain.Models;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepository<Usuario>
    {
        Usuario GetUser(string username);
        Usuario GetUserByEmail(string email);
        PagedList<Usuario> GetPagedList(int page = 0, int limit = 10);
    }
}