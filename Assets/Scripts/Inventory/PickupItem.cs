using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public InventoryManager InventoryManager;
    public ScriptableItem thisItem;

    private void Start()
    {
        InventoryManager = FindFirstObjectByType<InventoryManager>(FindObjectsInactive.Include);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerStateManager>() != null)
        {
            InventoryManager.AddItem(thisItem);
            this.gameObject.SetActive(false);
        }
    }
}
