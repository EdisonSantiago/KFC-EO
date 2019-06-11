using Microsoft.AspNet.Identity;
using NUnit.Framework;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models.Identity;
using Oulanka.Web.Core.Identity;
using Rhino.Mocks;

namespace Oulanka.Tests.Oulanka.Web.Core.Identity
{
    [TestFixture]
    public class UserStoreTests
    {
        private IIdentityUserService _identityUserServiceMock;
        private IIdentityRoleService _identityRoleServiceMock;
        private UserStore<IdentityUser> _userStore;

            [SetUp]
        public void SetUp()
        {
            _identityUserServiceMock = MockRepository.GenerateMock<IIdentityUserService>();
            _identityRoleServiceMock = MockRepository.GenerateMock<IIdentityRoleService>();

            _userStore = new UserStore<IdentityUser>();
        }

        [Test]
        [Ignore]
        public void User_does_not_exist()
        {
            var login = new UserLoginInfo("ProviderTest", "ProviderKey");
            var user = _userStore.FindAsync(login);

            Assert.IsNull(user);
        }
    }
}