using System;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CommonServiceLocator.WindsorAdapter;
using log4net.Config;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Configuration;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Infrastructure.NHibernateMaps;
using Oulanka.Services;
using Oulanka.Web.Core.Mvc;
using Oulanka.Web.Mvc.Models.Mappings;
using SharpArch.Domain.Events;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Web.Mvc;
using SharpArch.Web.Mvc.Castle;
using SharpArch.Web.Mvc.ModelBinder;
using SharpArchContrib.Data.NHibernate;
using Timer = System.Timers.Timer;

namespace Oulanka.Web.Mvc
{
    public class MvcApplication : HttpApplication
    {
        #region Fields

        private WebSessionStorage _webSessionStorage;
        private ThreadSessionStorage _threadSessionStorage;

        private static Timer _keepAliveTimer;
        private const int KeepAliveMinutes = 5;
        public event EventHandler KeepAliveElapsed;

        #endregion

        #region Public Methods and Operators

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

        #endregion

        #region Methods

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

        protected void Application_Start()
        {
            XmlConfigurator.Configure();

            ViewEngines.Engines.Clear();

            var razorViewEngine = new RazorViewEngine();
            razorViewEngine.ViewLocationCache = new TwoLevelViewCache(razorViewEngine.ViewLocationCache);
            ViewEngines.Engines.Add(razorViewEngine);

            ModelBinders.Binders.DefaultBinder = new SharpModelBinder();

            // ModelValidatorProviders.Providers.Add(new ClientDataTypeModelValidatorProvider());

            this.InitializeServiceLocator();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfiguration.Configure();

            JobsService.Instance().Start();

            if (KeepAliveMinutes > 0)
            {
                _keepAliveTimer = new Timer(60000 * KeepAliveMinutes);
                _keepAliveTimer.Elapsed += new ElapsedEventHandler(KeepAlive);
                _keepAliveTimer.Start();
            }

        }

        protected void Application_End()
        {
            JobsService.Instance().Stop();
        }

        /// <summary>
        ///     Instantiate the container and add all Controllers that derive from
        ///     WindsorController to the container.  Also associate the Controller
        ///     with the WindsorContainer ControllerFactory.
        /// </summary>
        protected virtual void InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Install(FromAssembly.This());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            var windsorServiceLocator = new WindsorServiceLocator(container);
            DomainEvents.ServiceLocator = windsorServiceLocator;
            ServiceLocator.SetLocatorProvider(() => windsorServiceLocator);
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

        #endregion


        #region sessions

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

        #endregion
    }
}