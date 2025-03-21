using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ScriptableItem item;

    public Image itemImage;

    public Transform DragEndParent;

    public void GetItemInfos(ScriptableItem newItem)
    {
        item = newItem;
        itemImage.sprite = newItem.itemImage;
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
