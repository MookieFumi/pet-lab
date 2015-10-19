using System;
using System.Configuration;

namespace api.pet.Infrastructure
{
    public static class Utilities
    {
        public static string GetAppUserName()
        {
            return Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["app.username"]);
        }

        public static string GetAppUserName2()
        {
            return ConfigurationManager.AppSettings["app.username"];
        }

        public static string GetAppUserName3()
        {
            return Environment.GetEnvironmentVariable("app.username");
        }

        public static string GetAppPassword()
        {
            return Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["app.password"]);
        }

        public static string GetAppRealm()
        {
            return Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["realm"]);
        }
    }
}