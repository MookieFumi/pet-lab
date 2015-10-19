using System;
using System.Configuration;

namespace api.pet.Infrastructure
{
    public static class Utilities
    {
        public static string GetAppUserName()
        {
            return ConfigurationManager.AppSettings["app.username"];
        }

        public static string GetAppPassword()
        {
            return ConfigurationManager.AppSettings["app.password"];
        }

        public static string GetAppRealm()
        {
            return ConfigurationManager.AppSettings["realm"];
        }
    }
}