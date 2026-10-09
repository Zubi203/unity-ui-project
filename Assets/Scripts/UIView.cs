using UnityEngine;
using System;
using System.Collections.Generic;
using ServiceLocatorPattern;
using TMPro;
using DG.Tweening;
using UnityEngine.InputSystem;

public class UIView : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject inventoryPanel;
    public GameObject shopPanel;
    [SerializeField] private TMP_Text moneyLabel;
    [SerializeField] private int moneyGain = 500;
    [SerializeField] private ParticleSystem moneyParticles;
    [SerializeField] private LevelLoader sceneLoader;
    Sequence seq;

    public event Action<int> AddMoney;
    public event Action InventoryUpdate;

    private void Awake()
    {
        ServiceLocator.Instance.Register(typeof(UIView), gameObject);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.Unregister(typeof(UIView));
    }

    public void SettingsButtonPressed()
    {
        if (settingsMenu == null) {return;}

        settingsMenu.SetActive(true);
    }

    public void MainMenuButtonPressed()
    {
        sceneLoader.TransitionToScene("TitleScreen");
    }

    public void AddMoneyButtonPressed()
    {
        AddMoney?.Invoke(moneyGain);

        if (moneyParticles == null){return;}
        moneyParticles.Stop();
        moneyParticles.Play();
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
