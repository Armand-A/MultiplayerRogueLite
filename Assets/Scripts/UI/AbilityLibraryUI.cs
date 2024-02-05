using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityLibraryUI : AbilityUI
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject panel;
    [SerializeField] Vector2 gap;
    UnityAction<Ability> returnAction;
    List<Ability> abilities = new List<Ability>();

    private void OnEnable()
    {
        PlayerAbilities playerAbilities = GameObject.FindObjectOfType<PlayerAbilities>();
        if (playerAbilities == null) return;
        abilities = playerAbilities.Abilities;

        Rect buttonRect = buttonPrefab.GetComponent<RectTransform>().rect;
        float rowWidth = abilities.Count * buttonRect.width + (abilities.Count - 1) * gap.x;
        float xCenterOffset = -(abilities.Count - 1) / 2f * (buttonRect.width + gap.x);

        for (int i = 0; i < abilities.Count; i++) {
            Ability ability = abilities[i];

            GameObject buttonObject = Instantiate(buttonPrefab);

            buttonObject.GetComponent<Button>().onClick.RemoveAllListeners();
            buttonObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClicked(ability));
            RectTransform buttonRectTransform = buttonObject.GetComponent<RectTransform>();
            buttonRectTransform.SetParent(panel.transform, false);
            buttonObject.transform.position = new Vector3(panel.transform.position.x + i * (buttonRect.width + gap.x) + xCenterOffset, panel.transform.position.y, 0);

            buttonObject.GetComponent<Image>().sprite = ability.Sprite;
        }
    }

    public void OnButtonClicked(Ability ability)
    {
        if (returnAction != null) returnAction.Invoke(ability);
        uiManager.CloseUI();
    }

    public void SetReturnAction(UnityAction<Ability> action)
    {
        returnAction = action;
    }

    public void OnCancel()
    {
        uiManager.CloseUI();
    }
}
