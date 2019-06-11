using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;

namespace Oulanka.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IGroupRepository _groupRepository;


        public UserService(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }
    }
}