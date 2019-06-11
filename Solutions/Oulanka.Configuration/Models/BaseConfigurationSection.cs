using System.Configuration;

namespace Oulanka.Configuration.Models
{
    public class BaseConfigurationSection : ConfigurationSection
    {

        #region Globals & Required

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        [ConfigurationProperty("applicationName", IsRequired = true)]
        public string ApplicationName => (string)this["applicationName"];

        /// <summary>
        /// Gets or sets the host path.
        /// </summary>
        /// <value>
        /// The host path.
        /// </value>
        [ConfigurationProperty("hostPath", IsRequired = true)]
        public string HostPath
        {
            get => (string)this["hostPath"];
            set => this["hostPath"] = value;
        }

        /// <summary>
        /// Gets a value indicating whether [send admin email on exception].
        /// </summary>
        /// <value>
        /// <c>true</c> if [send admin email on exception]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("sendAdminEmailOnException", DefaultValue = true)]
        public bool SendAdminEmailOnException => (bool)this["sendAdminEmailOnException"];

        #endregion

        #region BAckground Jobs




        #endregion

        #region Emails: Recipients - Senders - Smtp Servers

        /// <summary>
        /// Gets the email admin recipient.
        /// </summary>
        /// <value>
        /// The email admin recipient.
        /// </value>
        [ConfigurationProperty("emailAdminRecipient", IsRequired = true)]
        public string EmailAdminRecipient => (string)this["emailAdminRecipient"];

        /// <summary>
        /// Gets the email contact request recipient.
        /// </summary>
        /// <value>
        /// The email contact request recipient.
        /// </value>
        [ConfigurationProperty("emailContactRequestRecipient")]
        public string EmailContactRequestRecipient => (string)this["emailContactRequestRecipient"];

        /// <summary>
        /// Gets the email global recipient.
        /// </summary>
        /// <value>
        /// The email global recipient.
        /// </value>
        [ConfigurationProperty("emailGlobalRecipient", IsRequired = true)]
        public string EmailGlobalRecipient => (string)this["emailGlobalRecipient"];

        /// <summary>
        /// Gets the email global sender.
        /// </summary>
        /// <value>
        /// The email global sender.
        /// </value>
        [ConfigurationProperty("emailGlobalSender", IsRequired = true)]
        public string EmailGlobalSender => (string)this["emailGlobalSender"];

        /// <summary>
        /// Gets the SMTP server.
        /// </summary>
        /// <value>
        /// The SMTP server.
        /// </value>
        [ConfigurationProperty("smtpServer", IsRequired = true)]
        public string SmtpServer => (string)this["smtpServer"];

        [ConfigurationProperty("smtpServerConnectionLimit", IsRequired = true, DefaultValue = -1)]
        public int SmtpServerConnectionLimit => (int)this["smtpServerConnectionLimit"];

        /// <summary>
        /// Gets the SMTP server port.
        /// </summary>
        /// <value>The SMTP server port.</value>
        [ConfigurationProperty("smtpServerPort", IsRequired = true, DefaultValue = 25)]
        public int SmtpServerPort => (int)this["smtpServerPort"];

        [ConfigurationProperty("smtpServerUsername", IsRequired = false)]
        public string SmtpServerUsername => (string)this["smtpServerUsername"];

        [ConfigurationProperty("smtpServerPassword", IsRequired = false)]
        public string SmtpServerPassword => (string)this["smtpServerPassword"];

        #endregion

        #region Emails - Resources - Templates

        /// <summary>
        /// Gets the resources file.
        /// </summary>
        /// <value>
        /// The resources file.
        /// </value>
        [ConfigurationProperty("resourcesFile", IsRequired = true, DefaultValue = "resources.xml")]
        public string ResourcesFile => (string)this["resourcesFile"];

        /// <summary>
        /// Gets the resources file path.
        /// </summary>
        /// <value>The resources file path.</value>
        [ConfigurationProperty("resourcesFilePath", IsRequired = true, DefaultValue = "/content/resources")]
        public string ResourcesFilePath => (string)this["resourcesFilePath"];

        [ConfigurationProperty("filesPath", IsRequired = true, DefaultValue = "/files/helpDesk")]
        public string FilesPath => (string)this["filesPath"];

        /// <summary>
        /// Gets the email templates location.
        /// </summary>
        /// <value>
        /// The email templates location.
        /// </value>
        [ConfigurationProperty("emailTemplatesLocation", IsRequired = true, DefaultValue = "/content/templates/emails")]
        public string EmailTemplatesLocation => (string)this["emailTemplatesLocation"];

        #endregion

        #region Images - Files

        /// <summary>
        /// Gets the cache location.
        /// </summary>
        /// <value>
        /// The cache location.
        /// </value>
        [ConfigurationProperty("cacheLocation", IsRequired = true, DefaultValue = "/cache")]
        public string CacheLocation => (string)this["cacheLocation"];

        /// <summary>
        /// Gets the user files location.
        /// </summary>
        /// <value>
        /// The user files location.
        /// </value>
        [ConfigurationProperty("userFilesLocation", IsRequired = true, DefaultValue = "/files/user")]
        public string UserFilesLocation => (string)this["userFilesLocation"];

        [ConfigurationProperty("tempFilesLocation", IsRequired = true, DefaultValue = "files/temp")]
        public string TempFilesLocation => (string)this["tempFilesLocation"];

        #endregion

        #region Jobs

        /// <summary>
        /// Gets a value indicating whether [disable background threads].
        /// </summary>
        /// <value>
        /// <c>true</c> if [disable background threads]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("disableBackgroundThreads", IsRequired = true, DefaultValue = false)]
        public bool DisableBackgroundThreads => (bool)this["disableBackgroundThreads"];

        [ConfigurationProperty("jobs", IsRequired = true)]
        public JobsConfigurationElement Jobs => (JobsConfigurationElement)this["jobs"];

        #endregion



     

        [ConfigurationProperty("languagesFile")]
        public string LanguagesFile => (string) this["languagesFile"];

        [ConfigurationProperty("defaultLanguage")]
        public string DefaultLanguage => (string) this["defaultLanguage"];


        #region Active Directory
        
        [ConfigurationProperty("LdapAuthEnabled")]
        public bool LdapAuthEnabled => (bool) this["LdapAuthEnabled"];

        [ConfigurationProperty("LdapConnectionPath")]
        public string LdapConnectionPath => (string) this["LdapConnectionPath"];

        [ConfigurationProperty("LdapDomain")]
        public string LdapDomain => (string)this["LdapDomain"];

        [ConfigurationProperty("LdapDomainUser")]
        public string LdapDomainUser => (string)this["LdapDomainUser"];

        [ConfigurationProperty("LdapDomainPassword")]
        public string LdapDomainPassword => (string)this["LdapDomainPassword"];

        #endregion


    }
}