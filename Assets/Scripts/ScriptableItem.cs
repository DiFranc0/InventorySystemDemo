using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableItem", menuName = "Scriptable Objects/Item")]
public class ScriptableItem : ScriptableObject
{
    public Sprite itemImage;
    public string itemName;
    public string itemDescription;
    public bool stackableItem = true;
}
