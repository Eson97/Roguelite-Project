using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //TODO: crear UI para seleccionar el "saveSlot"

    [SerializeField] private int _saveSlot = -1;

    private const string SAVE_FILE_EXTENSION = ".json";

    private string FileName => $"Save{_saveSlot}{SAVE_FILE_EXTENSION}";
    private string AutoSaveFileName => $"Autosave{SAVE_FILE_EXTENSION}";

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

        SaveSystem.SaveFile(FileName, saveData);

        Debug.Log("Save Done!");
    }
    public void AutoSave()
    {
        SaveData saveData = new SaveData
        {
            Id = _saveSlot,
            Inventory = InventoryManager.Instance.SerializableInventoryItemData,
        };

        SaveSystem.SaveFile(AutoSaveFileName, saveData);

        Debug.Log("AutoSave Done!");
    }
    public void Load()
    {
        var saveData = SaveSystem.LoadFile(FileName);
        OnLoadData?.Invoke(saveData);

        Debug.Log("Load Done!");
    }

}

