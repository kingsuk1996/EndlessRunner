using System.IO;
using UnityEngine;

namespace EndlessRunner
{
    public static class SaveManager
    {
        private static readonly string filePath =
            Path.Combine(Application.persistentDataPath, "gamedata.json");

        public static GameData Load()
        {
            if (!File.Exists(filePath))
            {
                return new GameData();
            }

            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<GameData>(json);
        }

        public static void Save(GameData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, json);
        }
    }
}
