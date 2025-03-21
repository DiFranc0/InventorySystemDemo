using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] InventorySlots;

    public ScriptableItem[] ItemsToAdd;

    public HealthManager healthManager;

    public GameObject inventoryItemPrefab;

    public Image selectedItemImg;

    public TMP_Text selectedItemNameTxt;

    public TMP_Text selectedItemQuantityTxt;

    public TMP_Text selectedItemDescriptionTxt;

    public GameObject saveFeedback;

    private InventoryItem selectedItem;

    public static InventoryManager Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddItem(ScriptableItem item)
    {
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && 
                itemInSlot.item == item && 
                itemInSlot.item.stackableItem == true)
            {
                itemInSlot.quantity++;
                itemInSlot.UpdateItemQuantity();
                return;
            }
            
        }

        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnItemInSlot(item, slot, 0);
                return;
            }
        }

    }

    private void SpawnItemInSlot(ScriptableItem item, InventorySlot slot, int quantity)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        if (quantity > 0)
        {
            inventoryItem.quantity = quantity;
        }
        inventoryItem.GetItemInfos(item);

        
    }

    public void GetSelectedItem(InventoryItem item)
    {
        selectedItem = item;
        selectedItemNameTxt.text = item.itemName;
        selectedItemImg.sprite = item.itemImage.sprite;
        selectedItemDescriptionTxt.text = item.itemDescription;
        selectedItemQuantityTxt.text = $"Quantity: {item.quantity.ToString()}";
        selectedItemImg.gameObject.SetActive(true);
    } 

    public void UseItem()
    {
        switch(selectedItem.itemName){
            case "Health Potion":
                healthManager.GainHealth(0.5f);
                break;
            case "Mana Potion":
                break;
            case "Venom Potion":
                healthManager.DamageHealth(0.7f);
                break;
            case "Great Red Gem":
                break;
        }

        selectedItem.quantity--;

        if(selectedItem.quantity <= 0)
        {
            Destroy(selectedItem.gameObject);
            selectedItemImg.gameObject.SetActive(false);
        }
        else
        {
            selectedItem.UpdateItemQuantity();
            GetSelectedItem(selectedItem);
        }
    }

    public void SaveInventory(ref PlayerInventoryData playerInventoryList)
    {
        for(int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            InventoryItemsData itemInList = null;
            
            if (itemInSlot != null)
            {
                itemInList = playerInventoryList.items.Find(j => j.itemName == itemInSlot.itemName);
            }
            
            if (itemInSlot != null && itemInList == null)
            {
                InventoryItemsData itemData = new InventoryItemsData();

                itemData.itemName = itemInSlot.itemName;
                itemData.itemQuantity = itemInSlot.quantity;
                itemData.slotIndex = i;

                playerInventoryList.items.Add(itemData);
            }
            else if(itemInList != null) 
            {
                itemInList.itemQuantity = itemInSlot.quantity;
                itemInList.slotIndex = i;
            }
        } 

        StartCoroutine(SaveFeedback());
    }

    IEnumerator SaveFeedback()
    {
        saveFeedback.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        saveFeedback.SetActive(false);
    }

    public void LoadInventory(PlayerInventoryData playerInventoryList)
    {
        for (int i = 0;i < playerInventoryList.items.Count; i++)
        {
            string itemName = playerInventoryList.items[i].itemName;
            int quantity = playerInventoryList.items[i].itemQuantity;
            int slotIndex = playerInventoryList.items[i].slotIndex;
            
            SpawnItemInSlot(FindItemByName(itemName), InventorySlots[slotIndex], quantity);
        }

        
    }

    public ScriptableItem FindItemByName(string name)
    {
        for (int i = 0; i < ItemsToAdd.Length; i++)
        {
            if (ItemsToAdd[i].itemName == name)
            {
                return ItemsToAdd[i];
            }
        }

        return null;
    }
}
