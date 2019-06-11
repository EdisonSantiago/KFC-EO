using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CommonServiceLocator.WindsorAdapter;
using log4net.Config;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Api.Models.Mappings;
using Oulanka.Configuration;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Infrastructure.NHibernateMaps;
using Oulanka.Services;
using SharpArch.Domain.Events;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Web.Mvc;
using SharpArch.Web.Mvc.Castle;
using SharpArch.Web.Mvc.ModelBinder;
using SharpArchContrib.Data.NHibernate;
using Timer = System.Timers.Timer;

namespace Oulanka.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private WebSessionStorage _webSessionStorage;
        private ThreadSessionStorage _threadSessionStorage;

        private static Timer _keepAliveTimer;
        private const int KeepAliveMinutes = 5;
        public event EventHandler KeepAliveElapsed;


        /// <summary>
        ///     Due to issues on IIS7, the NHibernate initialization must occur in Init().
        ///     But Init() may be invoked more than once; accordingly, we introduce a thread-safe
        ///     mechanism to ensure it's only initialized once.
        ///     See http://msdn.microsoft.com/en-us/magazine/cc188793.aspx for explanation details.
        /// </summary>
        public override void Init()
        {
            base.Init();
            this._webSessionStorage = new WebSessionStorage(this);
            _threadSessionStorage = new ThreadSessionStorage();
        }

        protected void Application_Start()
        {
            XmlConfigurator.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            ModelBinders.Binders.DefaultBinder = new SharpModelBinder();
            InitializeServiceLocator();

            AutoMapperConfiguration.Configure();

            JobsService.Instance().Start();

            if (KeepAliveMinutes > 0)
            {
                _keepAliveTimer = new Timer(60000 * KeepAliveMinutes);
                _keepAliveTimer.Elapsed += new ElapsedEventHandler(KeepAlive);
                _keepAliveTimer.Start();
            }

        }

        private void InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Install(FromAssembly.This());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            var windsorServiceLocator = new WindsorServiceLocator(container);
            DomainEvents.ServiceLocator = windsorServiceLocator;
            ServiceLocator.SetLocatorProvider(() => windsorServiceLocator);
        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            NHibernateInitializer.Instance().InitializeNHibernateOnce(this.InitialiseNHibernateSessions);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var culture = new CultureInfo("es-EC");
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }

        
        protected void Application_Error(object sender, EventArgs e)
        {
            // Useful for debugging
            Exception ex = this.Server.GetLastError();
            var reflectionTypeLoadException = ex as ReflectionTypeLoadException;
        }

        protected void Application_End()
        {
            JobsService.Instance().Stop();
        }

        protected void Session_Start()
        {
           
        }

        protected void Session_End()
        {
            var userService = ServiceLocator.Current.GetInstance<IUserAccountService>();
            var username = (string)Session["HpDk_User"];
            if (!string.IsNullOrEmpty(username))
            {
                var user = userService.GetUser(username);
                user.EstaEnLinea = false;
                userService.SaveOrUpdateUser(user);
            }

            //FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();

        }

        private void InitialiseNHibernateSessions()
        {
            NHibernateSession.ConfigurationCache = new NHibernateConfigurationFileCache();

            NHibernateSession.Init(
                this._threadSessionStorage,
                new[] { this.Server.MapPath("~/bin/Oulanka.Infrastructure.dll") },
                new AutoPersistenceModelGenerator().Generate(),
                this.Server.MapPath("~/NHibernate.config"));
        }


        private void KeepAlive(object sender, ElapsedEventArgs e)
        {
            var configuration = ServiceLocator.Current.GetInstance<IConfigurationSettings>();
            var hostPath = configuration.GetConfig().HostPath;

            _keepAliveTimer.Enabled = false;
            if (KeepAliveElapsed != null)
                KeepAliveElapsed(this, e);

            _keepAliveTimer.Enabled = true;

            using (WebRequest.Create(hostPath).GetResponse()) { }
        }
    }
}
