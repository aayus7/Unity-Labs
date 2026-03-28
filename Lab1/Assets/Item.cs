using UnityEngine;

// Changed menuPath to menuName below
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea] public string description;
    public Sprite icon;
    public int value;
    public bool isStackable;
}