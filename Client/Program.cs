using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FightingGameClient.Data;
namespace FightingGameClient
{
    internal static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CharactersData.LoadCharacters("CharacterSettings.json");
            ServerSettings.JsonLoadSettings("serverSettings.json");
            if (CharactersData.Characters == null)
                throw new InvalidOperationException("Characters data is not loaded. Please ensure the JSON file is correctly loaded.");
            if (ServerSettings.Ip == null || ServerSettings.Port == null)
                throw new InvalidOperationException("Sever data is not loaded. Please ensure the JSON file is correctly loaded.");
            Application.Run(new MainForm());
        }
    }
}
