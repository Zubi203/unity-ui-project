using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(ScrollRect))]
public class PooledScrollView : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private RectTransform contentParent;
    [SerializeField] private float itemWidth = 150f;
    [SerializeField] private int bufferItems = 2;

    private List<InventoryItem> allElementData = new List<InventoryItem>();
    private List<ShopItem> cachedElements = new List<ShopItem>();

    private ScrollRect scrollRect;
    private int totalItems;
    private int visibleItems;
    private int firstVisibleItemIndex = -1;

    void Awake()
    {
        //get scroll rect
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.onValueChanged.AddListener(OnScroll);
    }

    public void Initialize(List<InventoryItem> itemData)
    {
        allElementData = itemData;
        totalItems = allElementData.Count;
        firstVisibleItemIndex = -1;

        //Set control parent width
        contentParent.sizeDelta = new Vector2(itemWidth * totalItems, contentParent.sizeDelta.y);

        //Calculate how many items fit into the viewport by referencing the viewport property of the scroll rect
        float viewportWidth = scrollRect.viewport.rect.width;
        visibleItems = Mathf.CeilToInt(viewportWidth / itemWidth) + bufferItems;
        
        //Clear pool in case it has any items in it
        foreach (ShopItem element in cachedElements)
        {
            if (element != null)
            {
                Destroy(element.gameObject);
            }
        }
        cachedElements.Clear();

        //Instantiate initial pool with some extra buffer items
        int itemsToSpawn = Mathf.Min(visibleItems, totalItems);
        for (int i = 0; i < itemsToSpawn; i++)
        {
            GameObject obj = Instantiate(itemPrefab, contentParent);
            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(1.7f, 1);
            rect.pivot = new Vector2(0.5f, 1);

            ShopItem shopItem = obj.GetComponent<ShopItem>();
            shopItem.SetData(allElementData[i]);
            cachedElements.Add(shopItem);

            obj.SetActive(false);
        }

        UpdateScrollView();
    }

    void OnScroll(Vector2 pos)
    {
        UpdateScrollView();
    }

    void UpdateScrollView()
    {
        if (allElementData.Count == 0) {return;}

        //Get content parent's anchor position
        float contentPosX = contentParent.anchoredPosition.x;

        int newFirstIndex = Mathf.FloorToInt(contentPosX / itemWidth);
        newFirstIndex = Mathf.Clamp(newFirstIndex, 0, totalItems - cachedElements.Count);

        if (newFirstIndex == firstVisibleItemIndex) return;

        firstVisibleItemIndex = newFirstIndex;

        for (int i = 0; i < cachedElements.Count; i++)
        {
            int dataIndex = firstVisibleItemIndex + i;

            if (dataIndex < totalItems)
            {
                cachedElements[i].gameObject.SetActive(true);
                RectTransform rect = cachedElements[i].GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(-dataIndex * itemWidth, 0);
                cachedElements[i].SetData(allElementData[dataIndex]);
            }
            else
            {
                cachedElements[i].gameObject.SetActive(false);
            }
        }
    }



}
