using System.Configuration;
using BCL.Configuration.Models;

namespace BCL.Configuration
{
    public static class ConfigFactory
    {
        private static GeneralSection _section;

        private static readonly object LockObject = new object();

        public static GeneralSection GetGeneralSection()
        {
            if (_section == null)
            {
                lock (LockObject)
                {
                    if (_section == null)
                    {
                        _section = (GeneralSection) ConfigurationManager.GetSection("generalSection");
                    }
                }
            }

            return _section;
        }
    }
}