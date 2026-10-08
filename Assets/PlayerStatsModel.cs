using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using ServiceLocatorPattern;

public class PlayerStatsModel : MonoBehaviour
{
    public event Action<int> HealthChanged;
    public event Action<int> MoneyChanged;

    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int startingCash = 5000;

    private int health;
    private int Health
    {
        get
        {
            return health;
        }
        
        set
        {
            health = value;
            health = Mathf.Clamp(health, 0, maxHealth);
            HealthChanged?.Invoke(health);
        }
    }

    private int money;
    public int Money
    {
        get
        {
            return money;
        }
        
        set
        {
            money = Mathf.Clamp(value, 0, 999999);
            MoneyChanged?.Invoke(money);
        }
    }

    [SerializeField] public Dictionary<InventoryItem, int> inventory = new Dictionary<InventoryItem, int>();


    public void AddInventoryItem(InventoryItem item)
    {
        if (!inventory.ContainsKey(item)) 
        {
            inventory.Add(item, 1);
        }
        else
        {
            inventory[item] += 1;
        }
    }

    public void RemoveInventoryItem(InventoryItem item)
    {
        if (inventory.ContainsKey(item)) 
        {
            inventory[item] -= 1;
            if (inventory[item] <= 0)
            {
                inventory.Remove(item);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
    }

    public void Heal(int amount)
    {
        Health += amount;
    }

    public void GainMoney(int amount)
    {
        Money += amount;
    }

    public void LoseMoney(int amount)
    {
        Money -= amount;
    }

    private void Awake()
    {
        ServiceLocator.Register(typeof(PlayerStatsModel), gameObject);

        
    }

    void Start()
    {
        StartCoroutine(GainStartingCash());
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister(typeof(PlayerStatsModel));
    }

    IEnumerator GainStartingCash()
    {
        yield return new WaitForEndOfFrame();
        GainMoney(startingCash);
    }
}
