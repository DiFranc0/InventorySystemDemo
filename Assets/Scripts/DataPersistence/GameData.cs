using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    
}

[System.Serializable]
public class InventoryItemsData
{
    public string itemName;
    public int itemQuantity;
    public int slotIndex;
}

[System.Serializable]

public class PlayerInventoryData
{
    public List<InventoryItemsData> items;

    public PlayerInventoryData()
    {
        items = new List<InventoryItemsData>();
    }

}
