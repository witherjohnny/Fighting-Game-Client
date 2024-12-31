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
            return BaseActionExists(personaggio, animationName) || AttackExists(personaggio,animationName) || ProjectileExists(personaggio, animationName);
        }
        // Check if an animation exists in BaseActions
        public static bool BaseActionExists(string characterName, string actionName)
        {
            Character character = Characters?.Find(c => c.Name == characterName);
            return character?.BaseActions.Contains(actionName) ?? false;
        }

        // Check if an animation exists in Attacks
        public static bool AttackExists(string characterName, string attackName)
        {
            Character character = Characters?.Find(c => c.Name == characterName);
            return character?.Attacks.Contains(attackName) ?? false;
        }

        // Check if an animation exists in Projectiles
        public static bool ProjectileExists(string characterName, string projectileName)
        {
            Character character = Characters?.Find(c => c.Name == characterName);
            return character?.Projectiles.Contains(projectileName) ?? false;
        }
    }
}
