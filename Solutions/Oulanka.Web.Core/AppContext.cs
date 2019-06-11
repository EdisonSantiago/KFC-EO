using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Web;

namespace Oulanka.Web.Core
{
    public sealed class AppContext
    {
        #region Properties & Fields

        private const string Datakey = "AppContextStore";

        public HybridDictionary Items { get; set; }

        public object this[string key]
        {
            get { return Items[key]; }
            set { Items[key] = value; }
        }

        public NameValueCollection QueryString { get; private set; }

        public HttpContext Context { get; private set; }
        public string SiteUrl { get; set; }

        public bool IsEmpty { get; set; }

        public bool IsWebRequest => Context != null;

        private string _rootPath;
        private string RootPath()
        {
            if (_rootPath == null)
            {
                _rootPath = AppDomain.CurrentDomain.BaseDirectory;
                var dirSep = Path.DirectorySeparatorChar.ToString();
                _rootPath = _rootPath.Replace("/", dirSep);
            }

            return _rootPath;
        }

        string _rawUrl;
        public Uri RawUrl
        {
            get
            {
                return new Uri(_rawUrl);
            }
            set
            {
                if (value != null) _rawUrl = value.ToString();
                else throw new ArgumentNullException(nameof(value));
            }
        }

        private Uri _currentUri;
        public Uri CurrentUri
        {
            get
            {
                if (_currentUri == null) _currentUri = new Uri("http://localhost/");
                return _currentUri;
            }

            set
            {
                _currentUri = value;
            }
        }

        private string _hostPath;
        public string HostPath
        {
            get
            {
                if (_hostPath == null)
                {
                    var portInfo = CurrentUri.Port == 80 ? string.Empty : string.Format(":{0}", CurrentUri.Port);
                    _hostPath = $"{CurrentUri.Scheme}://{CurrentUri.Host}{portInfo}";
                }

                return _hostPath;
            }
        }

        #endregion



        private AppContext(Uri uri, string siteUrl)
        {
            Items = new HybridDictionary();
            Initialize(new NameValueCollection(), uri, uri.ToString(), siteUrl);

        }

        private AppContext(HttpContext context, bool includeQueryStrings)
        {
            Items = new HybridDictionary();
            Context = context;

            if (includeQueryStrings)
                Initialize(
                          new NameValueCollection(context.Request.QueryString),
                          context.Request.Url,
                          context.Request.RawUrl,
                          GetSiteUrl());
            else
                Initialize(null, context.Request.Url, context.Request.RawUrl, GetSiteUrl());

        }

        private void Initialize(NameValueCollection queryString, Uri uri, string rawUrl, string siteUrl)
        {
            QueryString = queryString;
            SiteUrl = siteUrl;
            _currentUri = uri;
            _rawUrl = rawUrl;
        }


        #region Create

        public static AppContext CreateEmptyContext()
        {
            var context = new AppContext(new Uri("http://CreateEmptyContext"), "http://CreateEmptyContext");
            context.IsEmpty = true;
            SaveContextToStore(context);

            return context;
        }

        public static AppContext Create()
        {
            var context = new AppContext(new Uri("http://CreateContextBySettingsID"), "http://CreateContextBySettingsID");
            SaveContextToStore(context);
            return context;
        }


        public static AppContext Create(HttpContext context)
        {
            var itsmContext = new AppContext(context, true);
            SaveContextToStore(itsmContext);

            return itsmContext;
        }

        public static AppContext Create(Uri uri, string appName)
        {
            var context = new AppContext(uri, appName);
            SaveContextToStore(context);
            return context;
        }

        #endregion

        #region Utilities

        public string MapPath(string path)
        {
            return Context != null ? Context.Server.MapPath(path)
                   : PhysicalPath(path.Replace("/", Path.DirectorySeparatorChar.ToString()).Replace("~", string.Empty));
        }

        public string PhysicalPath(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");

            return string.Concat(
                                  RootPath().TrimEnd(Path.DirectorySeparatorChar),
                                  Path.DirectorySeparatorChar.ToString(),
                                  path.TrimStart(Path.DirectorySeparatorChar)
                                 );
        }


        #endregion

        #region Internals
        private static void SaveContextToStore(AppContext context)
        {
            if (context.IsWebRequest)
                context.Context.Items[Datakey] = context;
            else
                Thread.SetData(GetSlot(), context);
        }

        private static LocalDataStoreSlot GetSlot()
        {
            return Thread.GetNamedDataSlot(Datakey);
        }
        private string GetSiteUrl()
        {
            return GetSiteUrl(true);
        }

        private string GetSiteUrl(bool removeWww)
        {
            var hostName = removeWww
                                  ? Context.Request.Url.Host.Replace("www.", string.Empty)
                                  : Context.Request.Url.Host;

            var applicationPath = Context.Request.ApplicationPath;
            if (applicationPath.EndsWith("/"))
                applicationPath = applicationPath.Remove(applicationPath.Length - 1, 1);

            return hostName + applicationPath;
        }

        #endregion

        public static AppContext Current
        {
            get
            {
                var httpContext = HttpContext.Current;
                var context = httpContext.Cache != null
                    ? httpContext.Items[Datakey] as AppContext
                    : Thread.GetData(GetSlot()) as AppContext;

                if (context == null)
                {
                    if(httpContext == null)
                        throw new InvalidOperationException("No ItsmContext exists in the Current Application. AutoCreate fails since HttpContext.Current is not accessible.");

                    context = httpContext.Request != null
                        ? new AppContext(httpContext, true)
                        : new AppContext(httpContext, false);

                    SaveContextToStore(context);
                }

                return context;
            }
        }

    }
}