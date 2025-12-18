#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

namespace EndlessRunner.Editor
{
    public static class GameDataEditorMenu
    {
        private const string FILE_NAME = "gamedata.json";

        [MenuItem("Endless Runner/Delete Game Data")]
        private static void DeleteGameData()
        {
            string filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);

            if (!File.Exists(filePath))
            {
                EditorUtility.DisplayDialog(
                    "Delete Game Data",
                    "No saved game data file found.",
                    "OK"
                );
                return;
            }

            bool confirm = EditorUtility.DisplayDialog(
                "Delete Game Data",
                "Are you sure you want to delete the saved game data?\n\nThis cannot be undone.",
                "Delete",
                "Cancel"
            );

            if (!confirm)
                return;

            File.Delete(filePath);

            Debug.Log($"[EndlessRunner] Game data deleted: {filePath}");

            EditorUtility.DisplayDialog(
                "Delete Game Data",
                "Game data file deleted successfully.",
                "OK"
            );
        }
    }
}
#endif
