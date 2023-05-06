using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }


    /// <summary>
    /// Create or Override a save file
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="saveString"></param>
    public static void SaveFile(string fileName, string saveString) =>
        File.WriteAllText(SAVE_FOLDER + fileName, saveString);

    /// <summary>
    /// Get data from a savefile
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns>Null if not file was found</returns>
    public static string LoadFile(string fileName)
    {
        string result = null;

        if (File.Exists(SAVE_FOLDER + fileName))
            result = File.ReadAllText(SAVE_FOLDER + fileName);

        return result;
    }
}
