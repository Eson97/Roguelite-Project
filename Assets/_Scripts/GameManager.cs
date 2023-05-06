using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //TODO: crear UI para seleccionar el "saveSlot"

    [SerializeField] private int _saveSlot = -1;

    private const string SAVEFILE_EXTENSION = ".json";

    private string FileName => $"Save{_saveSlot}{SAVEFILE_EXTENSION}";
    private string AutoSaveFileName => $"Autosave{SAVEFILE_EXTENSION}";

    public event Action<SaveData> OnLoadData;

    protected override void Awake()
    {
        base.Awake();
        SaveSystem.Init();
    }
    public void SetSaveSlot(int saveSlot)=> _saveSlot = saveSlot;
    public void Save()
    {
        //no saveSlot selected
        if (_saveSlot < 0) return;

        SaveData saveData = new SaveData
        {
            Id = _saveSlot,
            Inventory = InventoryManager.Instance.SerializableInventoryItemData,
        };

        var json = Utilities.Json.Serialize(saveData);

        SaveSystem.SaveFile(FileName, json);
    }
    public void AutoSave()
    {
        SaveData saveData = new SaveData
        {
            Id = _saveSlot,
            Inventory = InventoryManager.Instance.SerializableInventoryItemData,
        };

        var json = Utilities.Json.Serialize(saveData);

        SaveSystem.SaveFile(AutoSaveFileName, json);
    }
    public void Load()
    {
        var json = SaveSystem.LoadFile(FileName);

        //no saveFile was found
        if (json == null) return;

        var saveData = Utilities.Json.Deserialize<SaveData>(json);
        OnLoadData?.Invoke(saveData);
    }

    public struct SaveData
    {
        public int Id { get; set; }
        public List<InventoryItemData> Inventory { get; set; }
    }
}

