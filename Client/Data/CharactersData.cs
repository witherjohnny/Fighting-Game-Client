using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGameClient.Data
{
    internal class CharactersData
    {
        public static List<Character> Characters { get; private set; } = new List<Character>();

        public static void LoadCharacters(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                Characters = JsonConvert.DeserializeObject<List<Character>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON: {ex.Message}");
            }
        }
    }
}
