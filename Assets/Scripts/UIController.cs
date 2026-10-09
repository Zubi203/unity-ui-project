using UnityEngine;
using ServiceLocatorPattern;

public class UIController : MonoBehaviour
{
    private PlayerStatsModel playerData
    {
        get
        {
            return ServiceLocator.Instance.GetService(typeof(PlayerStatsModel)).GetComponent<PlayerStatsModel>();
        }

    }

    private UIView mainUI
    {
        get
        {
            return ServiceLocator.Instance.GetService(typeof(UIView)).GetComponent<UIView>();
        }

    }

    private InventoryView inventoryUI
    {
        get
        {
            return ServiceLocator.Instance.GetService(typeof(InventoryView)).GetComponent<InventoryView>();
        }

    }

    private ShopView shopUI
    {
        get
        {
            return ServiceLocator.Instance.GetService(typeof(ShopView)).GetComponent<ShopView>();
        }

    }

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip purchaseSound;
    [SerializeField] private AudioClip purchaseFailSound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainUI.AddMoney += playerData.GainMoney;
        playerData.MoneyChanged += mainUI.OnMoneyChanged;
        mainUI.InventoryUpdate += UpdateInventoryView;
        shopUI.TryPurchaseItem += TryPurchase;
    }

    private void UpdateInventoryView()
    {
        inventoryUI?.UpdateInventory(playerData.inventory);
    }

    private void TryPurchase(InventoryItem item)
    {
        if (playerData.Money < item.price)
        {
            if (source != null && purchaseFailSound != null)
            {
                source.PlayOneShot(purchaseFailSound);
            }
            return;
        }

        if (source != null && purchaseSound != null)
        {
            source.PlayOneShot(purchaseSound);
        }

        playerData.LoseMoney(item.price);
        playerData.AddInventoryItem(item);
        UpdateInventoryView();
    }
}
