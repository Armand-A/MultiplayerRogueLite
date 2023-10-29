using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityUpgradeLibraryUI : AbilityUI
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject abilityButtonsContainer;
    [SerializeField] Vector2 gap;
    [SerializeField] AbilityUpgradeUI abilityUpgradeUI;
    List<AttackScriptableObject> abilities = new List<AttackScriptableObject>();
    PlayerAbilities playerAbilities; 

    private void OnEnable()
    {
        playerAbilities = GameObject.FindObjectOfType<PlayerAbilities>();
        UpdateUI();
    }

    private void UpdateUI()
    {
        abilities = playerAbilities.Abilities.FindAll((attack) => attack.NextUpgrade != null);

        Rect buttonRect = buttonPrefab.GetComponent<RectTransform>().rect;
        float rowWidth = abilities.Count * buttonRect.width + (abilities.Count - 1) * gap.x;
        float xCenterOffset = -(abilities.Count - 1) / 2 * (buttonRect.width + gap.x);

        foreach (Transform child in abilityButtonsContainer.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < abilities.Count; i++)
        {
            AttackScriptableObject ability = abilities[i];

            GameObject buttonObject = Instantiate(buttonPrefab);

            buttonObject.GetComponent<Button>().onClick.RemoveAllListeners();
            buttonObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClicked(ability));
            RectTransform buttonRectTransform = buttonObject.GetComponent<RectTransform>();
            buttonRectTransform.SetParent(abilityButtonsContainer.transform, false);
            buttonObject.transform.position = new Vector3(panel.transform.position.x + i * (buttonRect.width + gap.x) + xCenterOffset, panel.transform.position.y, 0);

            buttonObject.GetComponent<Image>().sprite = ability.Sprite;
        }
    }

    public void OnButtonClicked(AttackScriptableObject ability)
    {
        AbilityUpgradeUI abilityUpgradeUiObject = (AbilityUpgradeUI) uiManager.OpenUIAndGet(abilityUpgradeUI);
        abilityUpgradeUiObject.Initialize(ability, () =>
        {
            playerAbilities.UpgradeAbility(ability);
            UpdateUI();
        });
    }

    public void OnCancel()
    {
        uiManager.CloseUI();
    }
}
