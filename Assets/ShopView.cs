using UnityEngine;
using ServiceLocatorPattern;
using System;
using System.Collections.Generic;

public class ShopView : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> shopCollection = new List<InventoryItem>();
    [SerializeField] private Transform shopItemContainer;

    public event Action<InventoryItem> TryPurchaseItem;

    private List<ShopItem> shopItems = new List<ShopItem>();

    public void OnLeaveButtonPressed()
    {
        gameObject.SetActive(false);
    }



    void OnDestory()
    {
        ServiceLocator.Unregister(typeof(ShopView));
    }

    void Start()
    {

        ServiceLocator.Register(typeof(ShopView), gameObject);
        gameObject.SetActive(false);

        GetShopItemSlots();
        SetShopItemData();
    }

    void GetShopItemSlots()
    {
        if (shopItemContainer == null || shopItemContainer.childCount <= 0){return;}

        shopItems.Clear();

        for (int i = 0; i < shopItemContainer.childCount; i++)
        {
            Transform childTransform = shopItemContainer.GetChild(i);
            ShopItem shopItemChild = childTransform.GetComponent<ShopItem>();
            shopItems.Add(shopItemChild);
            shopItemChild.ItemPurchased += BuyButtonPressed;
        }
    }

    void SetShopItemData()
    {
        foreach (InventoryItem item in shopCollection)
        {
            AddItem(item);
        }
    }

    void AddItem(InventoryItem item)
    {
        foreach(ShopItem shopItem in shopItems)
        {
            if(shopItem.itemData == null)
            {
                shopItem.SetData(item);
                break;
            }
        }
    }

    void BuyButtonPressed (InventoryItem item)
    {
        if (item == null){return;}

        TryPurchaseItem?.Invoke(item);
    }

    


}
