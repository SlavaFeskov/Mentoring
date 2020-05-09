using System.Configuration;

namespace HttpModule.Task7.SiteDownloader.Model.Configuration
{
    public static class Configuration
    {
        public static string DefaultUserAgent => GetConfigValue("DefaultUserAgent");

        private static string GetConfigValue(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    }
}