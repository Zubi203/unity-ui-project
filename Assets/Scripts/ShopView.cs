using UnityEngine;
using ServiceLocatorPattern;
using System;
using System.Collections.Generic;

public class ShopView : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> shopCollection = new List<InventoryItem>();
    [SerializeField] private PooledScrollView scrollView;

    public event Action<InventoryItem> TryPurchaseItem;

    private List<ShopItem> shopItems = new List<ShopItem>();

    public void OnLeaveButtonPressed()
    {
        gameObject.SetActive(false);
    }



    void OnDestory()
    {
        ServiceLocator.Instance.Unregister(typeof(ShopView));
    }

    void Start()
    {

        ServiceLocator.Instance.Register(typeof(ShopView), gameObject);
        gameObject.SetActive(false);

        if (scrollView != null)
        {
            scrollView.Initialize(shopCollection);
        }
    }

    public void BuyButtonPressed (InventoryItem item)
    {
        if (item == null){return;}

        TryPurchaseItem?.Invoke(item);
    }

}
