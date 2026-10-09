using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    public string displayName = "";
    public string description = "";
    public Sprite sprite;
    public int price = 10;
    public int sellPrice {
        get
        {
            return (int)(price * 0.75);
        }
    }
}
