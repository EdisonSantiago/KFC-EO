using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IUserAccountService
    {
        Usuario GetUser(string username);
        IList<Usuario> GetUsers();
        Usuario GetUserById(int userId);
        Usuario GetUserByEmail(string email);

        IList<Usuario> GetUsersByGroupName(string groupName);
        Grupo GetGroupByName(string groupName);
        ActionConfirmation SaveOrUpdateUser(Usuario usuario);
        IList<Grupo> GetGroups();
        Grupo GetGroup(int roleId);
        ActionConfirmation SaveOrUpdateGroup(Grupo grupo);
        PagedList<Usuario> GetUsersPagedList(int page = 0, int limit = 10);
        ActionConfirmation SaveOrUpdateUserProfile(PerfilUsuario perfilUsuario);
        Usuario Login(string username, string password);
    }
}