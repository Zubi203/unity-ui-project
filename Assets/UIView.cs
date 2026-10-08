using UnityEngine;
using System;
using System.Collections.Generic;
using ServiceLocatorPattern;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class UIView : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject inventoryPanel;
    public GameObject shopPanel;
    [SerializeField] private TMP_Text moneyLabel;
    [SerializeField] private int moneyGain = 500;
    [SerializeField] private ParticleSystem moneyParticles;
    Sequence seq;

    public event Action<int> AddMoney;
    public event Action InventoryUpdate;

    private void Awake()
    {
        ServiceLocator.Register(typeof(UIView), gameObject);
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister(typeof(UIView));
    }

    public void SettingsButtonPressed()
    {
        if (settingsMenu == null) {return;}

        settingsMenu.SetActive(true);
    }

    public void MainMenuButtonPressed()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void AddMoneyButtonPressed()
    {
        AddMoney?.Invoke(moneyGain);
        moneyParticles?.Play();
    }

    public void OnMoneyChanged(int amount)
    {
        if (moneyLabel == null) {return;}

        moneyLabel.text = amount.ToString();

        if (seq != null && seq.IsActive())
        {
            seq.Kill();
        }
        seq = DOTween.Sequence();
        Transform moneyTransform = moneyLabel.GetComponent<Transform>();
        moneyTransform.localScale = Vector2.one;
        seq.Append(moneyTransform.DOPunchScale(Vector2.one * 0.5f, 0.2f).SetEase(Ease.OutBack));
        
    }

    public void InventoryButtonPressed()
    {
        if (inventoryPanel == null) {return;}

        InventoryUpdate?.Invoke();
        inventoryPanel.SetActive(true);
    }

    public void ShopButtonPressed()
    {
        if (shopPanel == null) {return;}

        shopPanel.SetActive(true);
    }
}
