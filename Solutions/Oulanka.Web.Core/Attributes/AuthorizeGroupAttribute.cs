using System;
using System.Web;
using System.Web.Mvc;
using Oulanka.Web.Core.Helpers;

namespace Oulanka.Web.Core.Attributes
{
    public class AuthorizeGroupAttribute : AuthorizeAttribute
    {
        private bool _isAuthenticated;
        private bool _isAuthorized;

        public string Groups { get; set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            if (_isAuthenticated && !_isAuthorized)
            {
                filterContext.Result = new RedirectResult("/error/notauthorized");
            }

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            _isAuthenticated = base.AuthorizeCore(httpContext);
            if (_isAuthenticated)
            {
                if (string.IsNullOrEmpty(Groups))
                {
                    _isAuthorized = true;
                    return _isAuthorized;
                }

                var groups = Groups.Split(',');
                var username = httpContext.User.Identity.Name;

                try
                {
                    _isAuthorized = GroupsHelper.UserIsMemberOfGroups(username, groups);
                    return _isAuthorized;
                }
                catch (Exception exception)
                {
                    throw exception;

                    //_isAuthorized = false;
                    //return _isAuthorized;
                }
            }

            _isAuthorized = false;

            return _isAuthorized;
        }
    }
}