using System;
using System.IO;
using System.Web;

namespace ProcureFlowX.Log
{
    public static class Logger
    {
        private static string GetErrorFilePath()
        {
            return HttpContext.Current.Server.MapPath("~/Data/Error.txt");
        }

        private static string GetSuccessFilePath()
        {
            return HttpContext.Current.Server.MapPath("~/Data/Success.txt");
        }

        public static void LogError(string message)
        {
            try
            {
                string path = GetErrorFilePath();
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine("--------------------------------------------------");
                    writer.WriteLine("Date: " + DateTime.Now.ToString());
                    writer.WriteLine("Error: " + message);
                    writer.WriteLine("--------------------------------------------------");
                }
            }
            catch
            {
                // Do nothing (avoid breaking app)
            }
        }

        public static void LogSuccess(string message)
        {
            try
            {
                string path = GetSuccessFilePath();
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine("--------------------------------------------------");
                    writer.WriteLine("Date: " + DateTime.Now.ToString());
                    writer.WriteLine("Message: " + message);
                    writer.WriteLine("--------------------------------------------------");
                }
            }
            catch
            {
                // Do nothing
            }
        }
    }
}