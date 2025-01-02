using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fighting_Game_Client.Data
{
    internal class ServerSettings
    {
        public static string Ip { get; set; } = null;
        public static int? Port { get; set; } = null;

        public static void JsonLoadSettings(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                ServerSettingsModel serverSettings = JsonConvert.DeserializeObject<ServerSettingsModel>(json);
                Ip = serverSettings.Ip;
                Port = serverSettings.Port;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON: {ex.Message}");
            }
        }
        private class ServerSettingsModel
        {
            public string Ip { get; set; }
            public int Port { get; set; }
        }
    }
}
