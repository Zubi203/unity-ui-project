using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class ShopItem : MonoBehaviour
{

    public InventoryItem itemData;

    [SerializeField] private InventorySlot itemSlot;
    [SerializeField] private TMP_Text purchaseCostLabel;

    public event Action<InventoryItem> ItemPurchased;
    
    public void SetData(InventoryItem item)
    {
        itemData = item;

        if (itemSlot == null || purchaseCostLabel == null){return;}

        itemSlot.SetItem(itemData, 1);

        if (itemData == null){return;}
        purchaseCostLabel.text = "$" + itemData.price.ToString();

    }

    public void OnBuyButtonPressed()
    {
        if (itemData == null){return;}
        ItemPurchased?.Invoke(itemData);
    }

    void Start()
    {
        SetData(itemData);
    }
}
