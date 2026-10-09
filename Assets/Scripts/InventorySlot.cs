using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryItem itemData = null;
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private TMP_Text tooltipText;
    [SerializeField] private GameObject tooltipPanel;

    public void SetItem(InventoryItem item, int amount)
    {
        if (iconImage == null || quantityText == null) {return;}
        if (tooltipText == null) {return;}

        itemData = item;

        if (amount > 0 && itemData != null)
        {
            if (amount == 1)
            {
                quantityText.text = "";
            }
            else
            {
                quantityText.text = amount.ToString();
            }
            iconImage.sprite = itemData.sprite;
            iconImage.CrossFadeAlpha(1.0f, 0.0f, true);
            tooltipText.text = "<b>" + itemData.displayName + ":</b>\n" + itemData.description;
        }
        else
        {
            quantityText.text = "";
            iconImage.sprite = null;
            iconImage.CrossFadeAlpha(0.0f, 0.0f, true);
            itemData = null;
            tooltipText.text = null;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemData == null || tooltipPanel == null) {return;}

        tooltipPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipPanel == null) {return;}

        tooltipPanel.SetActive(false);
    }
}
