using ExpenseTracker.Common.ConfigObjects;
using System;
using System.IO;

namespace ExpenseTracker.Common
{
    public class Config
    {
        public string SqlConnection { get; set; }
        public string ServerAddress { get; set; }
        public string ServerPort { get; set; }
        public string Token { get; set; }
        public MailConfig MailConfig { get; set; }
        public string ReportEmailRecipient { get; set; }

        public static Config Get
        {
            get
            {
                return GetConfig();
            }
        }

        private static Config GetConfig()
        {
            var configPath = "D:\\Code\\ExpenseTracker\\config.json";

            if (Environment.MachineName == "SERVER2019")
            {
                configPath = "D:\\APP_DEPLOY\\ExpenseTracker\\config.json";
            }

            if (!File.Exists(configPath))
            {
                throw new Exception($"Config file not exist, path: {configPath}, machine: {Environment.MachineName}");
            }

            var config = File.ReadAllText(configPath);
            return JsonUtility.ParseToObject<Config>(config);
        }
    }
}
