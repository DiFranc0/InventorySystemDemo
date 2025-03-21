using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] InventorySlots;

    public GameObject inventoryItemPrefab;

    public Image selectedItemImg;

    public TMP_Text selectedItemNameTxt;

    public TMP_Text selectedItemQuantityTxt;

    public TMP_Text selectedItemDescriptionTxt;

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
            }
            
        }

        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnItemInSlot(item, slot);
                return;
            }
        }

    }

    private void SpawnItemInSlot(ScriptableItem item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
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

    private void UseItem()
    {
        switch(selectedItem.itemName){
            case "Health Potion":
                break;
            case "Mana Potion":
                break;
            case "Venom Potion":
                break;
            case "Great Red Gem":
                break;
        }

        selectedItem.quantity--;

        if(selectedItem.quantity <= 0)
        {
            Destroy(selectedItem.gameObject);
        }
        else
        {
            selectedItem.UpdateItemQuantity();
            GetSelectedItem(selectedItem);
        }
    }

    public void SaveInventory()
    {

    }

    public void LoadInventory()
    {

    }
}
