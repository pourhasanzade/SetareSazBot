using System.Configuration;

namespace SetareSazBot.Utility
{
    public static class Variables
    {
        public static string GetValue(string key) => ConfigurationManager.AppSettings[key];

        public static string ConnectionString => ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public static string ReplyTimeout => GetValue("ReplyTimeout");
        public static string BotKey => GetValue("BotKey");
        public static string BotApi => GetValue("BotApi");

        public static string DownloadAddress = "~/Downloads/";

        public static string DoneImageUrl = "https://rubinobusinessbot.iranlms.ir/Images/check.png";
    }
}