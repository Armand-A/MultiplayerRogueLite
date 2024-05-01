using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using EntityDataEnums;
using UnityEngine.UI;

public class UpgradeUI : ShopUI
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject upgradeButtonsContainer;
    [SerializeField] Vector2 gap = new Vector2(16, 16);
    [SerializeField] protected UnityEvent<Upgrade> onUpgradeClicked;
    [SerializeField] Rarity rarityFilterType = Rarity.Common;

    public Upgrade SelectedUpgrade { get; set; }

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        PlayerUpgrades playerUpgrades = FindObjectOfType<PlayerUpgrades>();
        if (playerUpgrades == null) return;

        // handle abilities array filtering
        List<Upgrade> upgrades;
        switch (rarityFilterType)
        {
            case Rarity.Rare:
                /*abilities = playerAbilities.Upgrades.FindAll(
                    (ability) =>
                        ability.AcquisitionType == Ability.EAcquisitionType.Base
                        && ability.ImbueOptions != null
                        && ability.ImbueOptions.Count > 0
                );*/
                upgrades = playerUpgrades.Upgrades;
                break;
            case Rarity.Epic:
                upgrades = new List<Upgrade>();
                if (SelectedUpgrade == null) break;
                /*foreach (Rarity imbueOption in SelectedUpgrade.ImbueOptions)
                {
                    //abilities.Add(imbueOption.resultAbility);
                }*/
                break;
            default:
                upgrades = playerUpgrades.Upgrades;
                break;
        }

        // calculate numbers for arranging UI elements
        Rect buttonRect = buttonPrefab.GetComponent<RectTransform>().rect;
        float rowWidth = upgrades.Count * buttonRect.width + (upgrades.Count - 1) * gap.x;
        float xCenterOffset = -(upgrades.Count - 1) / 2f * (buttonRect.width + gap.x);

        foreach (Transform child in upgradeButtonsContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // instantiate upgrade buttons
        for (int i = 0; i < upgrades.Count; i++)
        {
            Upgrade upgrade = upgrades[i];

            GameObject buttonObject = Instantiate(buttonPrefab);
            buttonObject.GetComponent<Button>().onClick.RemoveAllListeners();
            buttonObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClicked(upgrade));

            RectTransform buttonRectTransform = buttonObject.GetComponent<RectTransform>();
            buttonRectTransform.SetParent(upgradeButtonsContainer.transform, false);
            buttonObject.transform.position = new Vector3(panel.transform.position.x + i * (buttonRect.width + gap.x) + xCenterOffset, panel.transform.position.y, 0);

            //buttonObject.GetComponent<Image>().sprite = upgrade.Sprite;
        }
    }

    public void OnButtonClicked(Upgrade upgrade)
    {
        onUpgradeClicked?.Invoke(upgrade);
    }

    public void AddOnUpgradeClickedListener(UnityAction<Upgrade> action)
    {
        onUpgradeClicked.AddListener(action);
    }

    public void RemoveOnUpgradeClickedListener(UnityAction<Upgrade> action)
    {
        onUpgradeClicked.RemoveListener(action);
    }
}