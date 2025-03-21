using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ScriptableItem item;

    public Image itemImage;
    public string itemName;
    public string itemDescription;
    public TMP_Text quantityTxt;
    public int quantity = 1;

    public Transform DragEndParent;

    public void GetItemInfos(ScriptableItem newItem)
    {
        item = newItem;
        itemImage.sprite = item.itemImage;
        itemName = newItem.itemName;
        itemDescription = newItem.itemDescription;
        UpdateItemQuantity();
    }

    public void UpdateItemQuantity()
    {
        quantityTxt.text = quantity.ToString();
        bool textActive = quantity > 1;
        quantityTxt.gameObject.SetActive(textActive);
    }

    public void OnClickOnItem()
    {
        InventoryManager.Instance.GetSelectedItem(this);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemImage.raycastTarget = false;
        DragEndParent = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemImage.raycastTarget = true;
        transform.SetParent(DragEndParent);
    }

}
