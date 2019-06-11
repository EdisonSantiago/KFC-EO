using System.Collections.Generic;
using System.Linq;
using Oulanka.Domain;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models;

namespace Oulanka.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public UserAccountService(
            IGroupRepository groupRepository, 
            IUserRepository userRepository,
            IUserProfileRepository userProfileRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
        }

        public Usuario GetUser(string username)
        {
            return _userRepository.GetUser(username);
        }

        public IList<Usuario> GetUsers()
        {
            return _userRepository.GetAll().OrderBy(x => x.NombreMostrar).ToList();
        }

        public Usuario GetUserById(int userId)
        {
            return _userRepository.Get(userId);
        }

        public Usuario GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public IList<Usuario> GetUsersByGroupName(string groupName)
        {
            return _groupRepository.GetByName(groupName).Usuarios;
        }

        public Grupo GetGroupByName(string groupName)
        {
            return _groupRepository.GetByName(groupName);
        }

        public ActionConfirmation SaveOrUpdateUser(Usuario usuario)
        {
            if (!usuario.IsValid()) return ActionConfirmation.CreateFailure("invalid object");

            try
            {
                _userRepository.SaveOrUpdate(usuario);
                _userRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("ok");
            }
            catch (System.Exception exception)
            {
                return ActionConfirmation.CreateFailure(exception.ToString());
            }

        }

        public IList<Grupo> GetGroups()
        {
            return _groupRepository.GetAll().OrderBy(x=>x.Descripcion).ToList();
        }

        public Grupo GetGroup(int roleId)
        {
            return _groupRepository.Get(roleId);
        }

        public ActionConfirmation SaveOrUpdateGroup(Grupo grupo)
        {
            if(!grupo.IsValid()) return ActionConfirmation.CreateFailure("not valid");

            try
            {
                _groupRepository.SaveOrUpdate(grupo);
                _groupRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (System.Exception exception)
            {
                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public PagedList<Usuario> GetUsersPagedList(int page = 0, int limit = 10)
        {
            return _userRepository.GetPagedList(page, limit);
        }

        public ActionConfirmation SaveOrUpdateUserProfile(PerfilUsuario perfilUsuario)
        {
            try
            {
                _userProfileRepository.SaveOrUpdate(perfilUsuario);
                _userProfileRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (System.Exception exception)
            {
                return ActionConfirmation.CreateFailure(exception.ToString());
            }

        }

        public Usuario Login(string username, string password)
        {
            var user = _userRepository.GetUser(username);
            if (user != null)
            {
                return user.LocalPassword == password ? user : null;
            }

            return null;
        }
    }
}