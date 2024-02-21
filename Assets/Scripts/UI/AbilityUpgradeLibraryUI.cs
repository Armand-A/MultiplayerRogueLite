//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;

//public class AbilityUpgradeLibraryUI : AbilityUI
//{
//    [SerializeField] GameObject buttonPrefab;
//    [SerializeField] GameObject panel;
//    [SerializeField] GameObject abilityButtonsContainer;
//    [SerializeField] Vector2 gap;
//    [SerializeField] AbilityUpgradePreviewUI abilityUpgradeUI;
//    List<Ability> abilities = new List<Ability>();
//    PlayerAbilities playerAbilities; 

//    private void OnEnable()
//    {
//        playerAbilities = GameObject.FindObjectOfType<PlayerAbilities>();
//        UpdateUI();
//    }

//    private void UpdateUI()
//    {
//        abilities = playerAbilities.Abilities.FindAll(
//            (ability) => ability.ImbueOptions != null && ability.ImbueOptions.Count > 0
//        );

//        Rect buttonRect = buttonPrefab.GetComponent<RectTransform>().rect;
//        float rowWidth = abilities.Count * buttonRect.width + (abilities.Count - 1) * gap.x;
//        float xCenterOffset = -(abilities.Count - 1) / 2 * (buttonRect.width + gap.x);

//        foreach (Transform child in abilityButtonsContainer.transform)
//        {
//            Destroy(child.gameObject);
//        }

//        for (int i = 0; i < abilities.Count; i++)
//        {
//            Ability ability = abilities[i];

//            GameObject buttonObject = Instantiate(buttonPrefab);

//            Button button = buttonObject.GetComponent<Button>();
//            button.onClick.RemoveAllListeners();
//            button.onClick.AddListener(() => OnButtonClicked(ability));

//            RectTransform buttonRectTransform = buttonObject.GetComponent<RectTransform>();
//            buttonRectTransform.SetParent(abilityButtonsContainer.transform, false);
//            buttonObject.transform.position = new Vector3(panel.transform.position.x + i * (buttonRect.width + gap.x) + xCenterOffset, panel.transform.position.y, 0);

//            buttonObject.GetComponent<Image>().sprite = ability.Sprite;
//        }
//    }

//    public void OnButtonClicked(Ability ability)
//    {
//        AbilityUpgradePreviewUI abilityUpgradeUiObject = (AbilityUpgradePreviewUI) uiManager.OpenUIAndGet(abilityUpgradeUI);
//        abilityUpgradeUiObject.Initialize(ability, () =>
//        {
//            if (playerAbilities.UpgradeAbility(ability))
//            {
//                uiManager.CloseUI();
//            } else
//            {
//                abilityUpgradeUiObject.ShowError("Insufficient currency");
//            }
//            UpdateUI();
//        });
//    }

//    public void OnCancel()
//    {
//        uiManager.CloseUI();
//    }
//}
