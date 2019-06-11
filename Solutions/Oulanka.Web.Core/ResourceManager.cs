using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Caching;
using System.Xml;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Configuration;
using Oulanka.Domain;
using Oulanka.Domain.Common;
using Oulanka.Domain.Enums;

namespace Oulanka.Web.Core
{
    public class ResourceManager
    {
        private static readonly IConfigurationSettings Configuration =
            ServiceLocator.Current.GetInstance<IConfigurationSettings>();

        private static readonly string _languagesFile = Configuration.GetConfig().LanguagesFile;


        

        public static NameValueCollection GetSupportedLanguages()
        {
            var context = AppContext.Current;

            const string cacheKey = "App-SupportedLanguages";

            var supportedLanguages = AppCache.Get(cacheKey) as NameValueCollection;
            if (supportedLanguages == null)
            {
                var filePath = context.MapPath(_languagesFile);
                var dp = new CacheDependency(filePath);
                supportedLanguages = new NameValueCollection();

                var d = new XmlDocument();
                d.Load(filePath);

                foreach (XmlNode n in d.SelectSingleNode("root").ChildNodes)
                {
                    if (n.NodeType != XmlNodeType.Comment)
                    {
                        supportedLanguages.Add(n.Attributes["key"].Value, n.Attributes["name"].Value);
                    }
                }

                AppCache.Max(cacheKey, supportedLanguages, dp);
            }

            return supportedLanguages;
        }

        public static string GetSupportedLanguage(string language)
        {
            return GetSupportedLanguage(language, Configuration.GetConfig().DefaultLanguage);
        }

        public static string GetSupportedLanguage(string language, string languageDefault)
        {
            var supportedLanguages = GetSupportedLanguages();
            var supportedLanguage = supportedLanguages[language];

            return !string.IsNullOrEmpty(supportedLanguage) ? language : languageDefault;
        }


        #region GetString

        public static string GetString(string name)
        {

            return GetString(name, false);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultOnly">if set to <c>true</c> [default only].</param>
        /// <returns></returns>
        public static string GetString(string name, bool defaultOnly)
        {
            return GetString(name, "Resources.xml", defaultOnly);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static string GetString(string name, string fileName)
        {
            return GetString(name, fileName, false);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="defaultOnly">if set to <c>true</c> [default only].</param>
        /// <returns></returns>
        public static string GetString(string name, string fileName, bool defaultOnly)
        {
            var context = AppContext.Current;
            //  string userLanguage = ItsmContext.User.Profile.Language;

            var resources = !string.IsNullOrEmpty(fileName)
                                ? GetResource(ResourceManagerType.String, Configuration.GetConfig().DefaultLanguage, fileName, defaultOnly)
                                : GetResource(ResourceManagerType.String, Configuration.GetConfig().DefaultLanguage, "Resources.xml", defaultOnly);

            var text = resources[name] as string;

            //try the standard file if we passed a file that didnt have the key we were looking for
            if (string.IsNullOrEmpty(text) == false && string.IsNullOrEmpty(fileName))
            {
                resources = GetResource(ResourceManagerType.String, Configuration.GetConfig().DefaultLanguage, "Resources.xml", false);

                text = resources[name] as string;
            }

            if (text == null)
            {

            }
            return text;
        }

        #endregion

        #region GetMessage

        public static Message GetMessage(string messageKey)
        {

            var resources = GetResource(ResourceManagerType.ErrorMessage, Configuration.GetConfig().DefaultLanguage, "Messages.xml", false);

            if (resources[messageKey] == null)
            {
                // LN 6/9/04: Changed to throw a forums exception 
                throw new Exception($"Value not found in Messages.xml for: {messageKey}");
            }

            return (Message)resources[messageKey];
        }

        public static string GetActionMessage(string messageKey, PageMessageType messageType)
        {
            var message = GetMessage(messageKey);
            var messageTemplate = GetString("Messages_Template");

            var fl = new FormatList();
            fl.Add("messageType", Enum.GetName(messageType.GetType(), messageType).ToLower());
            fl.Add("messageTitle", message.Title);
            fl.Add("messageBody", message.Body);

            return fl.Format(messageTemplate);
        }

        public static string GetActionMessage(string title, string body, PageMessageType messageType)
        {
            return GetActionMessage(title, body, string.Empty, messageType);
        }

        public static string GetActionMessage(string title, string body, string extraBody, PageMessageType messageType)
        {
            var messageTemplate = GetString("Messages_Template");

            var fl = new FormatList();
            fl.Add("messageType", Enum.GetName(messageType.GetType(), messageType).ToLower());
            fl.Add("messageTitle", title);
            fl.Add("messageBody", body);
            fl.Add("messageExtraText", extraBody);

            return fl.Format(messageTemplate);
        }


        #endregion

        #region GetResource & Load Resource

        private static Hashtable GetResource(ResourceManagerType resourceType, string userLanguage, string fileName, bool defaultOnly)
        {


            var defaultLanguage = Configuration.GetConfig().DefaultLanguage;
            var cacheKey = resourceType + defaultLanguage + userLanguage + fileName;

            // Ensure the user has a language set
            //
            if (string.IsNullOrEmpty(userLanguage) || defaultOnly)
                userLanguage = defaultLanguage;

            // Attempt to get the resources from the Cache
            //
            var resources = AppCache.Get(cacheKey) as Hashtable;

            if (resources == null)
            {
                resources = new Hashtable();

                // First load the English resouce, changed from loading the default language
                // since the userLanguage is set to the defaultLanguage if the userLanguage
                // is unassigned. We load the english language always just to ensure we have
                // a resource loaded just incase the userLanguage doesn't have a translated
                // string for this English resource.
                //
                resources = LoadResource(resourceType, resources, "es-EC", cacheKey, fileName);

                // If the user language is different load it
                //
                if ("es-EC" != userLanguage)
                    resources = LoadResource(resourceType, resources, userLanguage, cacheKey, fileName);

            }

            return resources;
        }

        private static Hashtable LoadResource(ResourceManagerType resourceType, Hashtable target, string language, string cacheKey, string fileName)
        {
            var filePath = AppContext.Current.PhysicalPath($"Languages\\{language}\\{{0}}");

            switch (resourceType)
            {
                case ResourceManagerType.ErrorMessage:
                    filePath = string.Format(filePath, "Messages.xml");
                    break;

                case ResourceManagerType.String:
                    filePath = string.Format(filePath, fileName);
                    break;

                default:

                    filePath = string.Format(filePath, "Resources.xml");
                    break;
            }

            var dp = new CacheDependency(filePath);

            var d = new XmlDocument();
            try
            {
                d.Load(filePath);
            }
            catch (XmlException)
            {
                return target;
            }

            foreach (var n in
                from XmlNode n in d.SelectSingleNode("root").ChildNodes
                where n.NodeType != XmlNodeType.Comment
                select n)
            {
                switch (resourceType)
                {
                    case ResourceManagerType.ErrorMessage:
                        var m = new Message(n);
                        target[m.MessageId] = m;
                        break;

                    case ResourceManagerType.String:
                        if (target[n.Attributes["name"].Value] == null)
                            target.Add(n.Attributes["name"].Value, n.InnerText);
                        else
                            target[n.Attributes["name"].Value] = n.InnerText;
                        break;
                }
            }

            // Create a new cache dependency and set it to never expire
            // unless the underlying file changes
            //
            // 7/26/2004 Terry Denham
            // We should only keep the default language cached forever, not every language.
            //DateTime cacheUntil;
            if (language == Configuration.GetConfig().DefaultLanguage)
                AppCache.Max(cacheKey, target, dp);
            else
                AppCache.Insert(cacheKey, target, dp, AppCache.HourFactor);


            return target;

        }


        #endregion
    }
}