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
        public static List<Character> Characters { get; private set; } = null;

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
        public static bool animationExists(string personaggio,string animationName)
        {
            
                Character character = Characters.Find(c => c.Name == personaggio);
                foreach (string action in character.BaseActions)
                {
                    if (action == animationName)
                    {
                        return true;
                    }
                }
                foreach (string action in character.Attacks)
                {
                    if (action == animationName)
                    {
                        return true;
                    }
                }
            
            return false;
        }
    }
}
