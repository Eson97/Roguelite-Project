using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using UnityEngine;

public class SaveSystem
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }


    public static void SaveFile(string fileName, SaveData saveData)
    {
        var saveString = JsonSerializer.Serialize(saveData);
        File.WriteAllText(SAVE_FOLDER + fileName, saveString);
    }

    public static SaveData LoadFile(string fileName)
    {
        if (!File.Exists(SAVE_FOLDER + fileName)) throw new FileNotFoundException();

        var text = File.ReadAllText(SAVE_FOLDER + fileName);
        var json = JsonSerializer.Deserialize<SaveData>(text);

        var data = new SaveData
        {
            Id = json.Id,
            Inventory = json.Inventory,
        };

        return data;
    }
}

public struct SaveData
{
    public int Id { get; set; }
    public List<InventoryItemData> Inventory { get; set; }
}

public struct InventoryItemData
{
    public string Name { get; set; }
    public int Quantity { get; set; }
}
