using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO; // Thêm namespace này

public class SaveGame : MonoBehaviour
{
    public static void SaveGameState()
    {
        SaveCurrentScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game saved successfully!");
    }

    private static void SaveCurrentScene(string sceneName)
    {
        string filePath = Application.persistentDataPath + "/CurrentScene.txt";

        File.WriteAllText(filePath, sceneName);
        Debug.Log("Save current scene: " + sceneName);
    }
    public static void LoadSavedScene()
    {
        string filePath = Application.persistentDataPath + "/CurrentScene.txt";
        if (File.Exists(filePath))
        {
            string level = File.ReadAllText(filePath);
            SceneManager.LoadScene(level);
            Debug.Log("Load scene: " + level);
        }
        else
        {
            Debug.LogWarning("Không tìm thấy");
        }
    }
}
