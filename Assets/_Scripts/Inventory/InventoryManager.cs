using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    //TODO: Agregar funciones para agregar o eliminar objetos del inventario

    public InventoryItemSO TestSeedSO;
    public InventoryItemSO TestCropSO;

    private Dictionary<string, InventoryItem> _inventoryItemsDic;

    public List<InventoryItem> InventoryItems => 
        _inventoryItemsDic.Values.Where(x => x.Quantity > 0).ToList();
    public List<InventoryItemData> SerializableInventoryItemData =>
        InventoryItems.Select(x => new InventoryItemData { Name = x.Data.Name, Quantity = x.Quantity }).ToList();

    protected override void Awake()
    {
        base.Awake();
        _inventoryItemsDic = new();
        Init();
    }
    private void Start()
    {
        GameManager.Instance.OnLoadData += GameManager_OnLoadData;
    }

    public void AddItem(InventoryItemSO itemData) => AddItem(itemData, 1);
    public void AddItem(InventoryItemSO itemData, int quantity)=>
        _inventoryItemsDic[itemData.Name].Add(quantity);
    public void RemoveItem(InventoryItemSO itemData)=> RemoveItem(itemData, 1);
    public void RemoveItem(InventoryItemSO itemData, int quantity)=>
        _inventoryItemsDic[itemData.Name].Remove(quantity);

    private void GameManager_OnLoadData(SaveData saveData) => 
        SetInventoryData(saveData.Inventory);
    private void SetInventoryData(List<InventoryItemData> data) => 
        data.ForEach(item => _inventoryItemsDic[item.Name].Quantity = item.Quantity);

    private void Init()
    {
        var items = Resources.LoadAll<InventoryItemSO>("ScriptableObjects").ToList();

        foreach (var item in items)
            _inventoryItemsDic[item.Name] = new InventoryItem { Data = item, Quantity = 0 };
    }
}
