using System.Web.Http;
using System.Web.Mvc;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Web.Core.Controllers;

namespace Oulanka.Api.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserAccountService _userAccountService;

        public UsersController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        // GET
        public IHttpActionResult Get(string username)
        {
            var user = _userAccountService.GetUser(username);
            return Ok(user);
        } 
    }
}
