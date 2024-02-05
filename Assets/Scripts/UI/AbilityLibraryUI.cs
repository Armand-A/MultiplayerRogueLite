using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static AbilityLibraryUI;

public class AbilityLibraryUI : AbilityUI
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject abilityButtonsContainer;
    [SerializeField] Vector2 gap = new Vector2(16, 16);
    [SerializeField] protected UnityEvent<Ability> onAbilityClicked;
    [SerializeField] AbilitiesFilterType abilitiesFilterType = AbilitiesFilterType.All;


    public enum AbilitiesFilterType
    {
        All, 
        Imbueable, 
        ImbueOptions
    }
    public Ability AbilityOfImbueOptions { get; set; }

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        PlayerAbilities playerAbilities = FindObjectOfType<PlayerAbilities>();
        if (playerAbilities == null) return;

        // handle abilities array filtering
        List<Ability> abilities;
        switch (abilitiesFilterType)
        {
            case AbilitiesFilterType.Imbueable:
                abilities = playerAbilities.Abilities.FindAll(
                    (ability) => 
                        ability.AcquisitionType == Ability.EAcquisitionType.Base
                        && ability.ImbueOptions != null
                        && ability.ImbueOptions.Count > 0
                );
                break;
            case AbilitiesFilterType.ImbueOptions:
                abilities = new List<Ability>();
                if (AbilityOfImbueOptions == null) break;
                foreach (Ability.ImbueOption imbueOption in AbilityOfImbueOptions.ImbueOptions)
                {
                    abilities.Add(imbueOption.resultAbility);
                }
                break;
            default:
                abilities = playerAbilities.Abilities;
                break;
        }

        // calculate numbers for arranging UI elements
        Rect buttonRect = buttonPrefab.GetComponent<RectTransform>().rect;
        float rowWidth = abilities.Count * buttonRect.width + (abilities.Count - 1) * gap.x;
        float xCenterOffset = -(abilities.Count - 1) / 2f * (buttonRect.width + gap.x);

        foreach (Transform child in abilityButtonsContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // instantiate ability buttons
        for (int i = 0; i < abilities.Count; i++) {
            Ability ability = abilities[i];

            GameObject buttonObject = Instantiate(buttonPrefab);
            buttonObject.GetComponent<Button>().onClick.RemoveAllListeners();
            buttonObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClicked(ability));

            RectTransform buttonRectTransform = buttonObject.GetComponent<RectTransform>();
            buttonRectTransform.SetParent(abilityButtonsContainer.transform, false);
            buttonObject.transform.position = new Vector3(panel.transform.position.x + i * (buttonRect.width + gap.x) + xCenterOffset, panel.transform.position.y, 0);

            buttonObject.GetComponent<Image>().sprite = ability.Sprite;
        }
    }

    public void OnButtonClicked(Ability ability)
    {
        onAbilityClicked?.Invoke(ability);
        // uiManager.CloseUI();
    }

    public void AddOnAbilityClickedListener(UnityAction<Ability> action)
    {
        onAbilityClicked.AddListener(action);
    }


    public void RemoveOnAbilityClickedListener(UnityAction<Ability> action)
    {
        onAbilityClicked.RemoveListener(action);
    }

    public void CloseUI()
    {
        uiManager.CloseUI();
    }
}
