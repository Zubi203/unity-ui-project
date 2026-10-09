using UnityEngine;
using ServiceLocatorPattern;
using System;
using System.Collections.Generic;

public class InventoryView : MonoBehaviour
{
    private List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] private Transform slotsContainer;

    public void OnBackButtonPressed()
    {
        gameObject.SetActive(false);
    }

    void Awake()
    {
        ServiceLocator.Instance.Register(typeof(InventoryView), gameObject);
    }

    void OnDestory()
    {
        ServiceLocator.Instance.Unregister(typeof(InventoryView));
    }


    private void GetSlots()
    {
        if(slotsContainer == null){return;}

        slots.Clear();

        for (int i = 0; i < slotsContainer.childCount; i++)
        {
            InventorySlot childSlot = slotsContainer.GetChild(i).GetComponent<InventorySlot>();
            if (childSlot == null) {continue;}
            slots.Add(childSlot);
        }
    }

    public void UpdateInventory(Dictionary<InventoryItem, int> inventory)
    {
        GetSlots();


        foreach(InventorySlot slot in slots)
        {
            slot.SetItem(null, 0);
        }

        foreach(InventoryItem item in inventory.Keys)
        {
            AddItem(item, inventory[item]);
        }
    }

    private void AddItem(InventoryItem item, int amount)
    {
        InventorySlot targetSlot = null;
        foreach(InventorySlot slot in slots)
        {
            if (slot.itemData == null)
            {
                targetSlot = slot;
                break;
            }
        }
        targetSlot.SetItem(item, amount);
    }

    void Start()
    {
        gameObject.SetActive(false);
    }


}
