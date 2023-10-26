using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityLibraryUI : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject panel;
    [SerializeField] Vector2 gap;
    UnityAction<AttackScriptableObject> returnAction;
    List<AttackScriptableObject> abilities = new List<AttackScriptableObject>();

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;

        PlayerAbilities playerAbilities = GameObject.FindObjectOfType<PlayerAbilities>();
        if (playerAbilities == null) return;
        abilities = playerAbilities.Abilities;

        Rect buttonRect = buttonPrefab.GetComponent<RectTransform>().rect;
        float rowWidth = abilities.Count * buttonRect.width + (abilities.Count - 1) * gap.x;
        float xCenterOffset = -(abilities.Count - 1) / 2 * (buttonRect.width + gap.x);

        for (int i = 0; i < abilities.Count; i++) {
            AttackScriptableObject ability = abilities[i];

            GameObject buttonObject = Instantiate(buttonPrefab);

            buttonObject.GetComponent<Button>().onClick.RemoveAllListeners();
            buttonObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClicked(ability));
            RectTransform buttonRectTransform = buttonObject.GetComponent<RectTransform>();
            buttonRectTransform.SetParent(panel.transform, false);
            buttonObject.transform.position = new Vector3(panel.transform.position.x + i * (buttonRect.width + gap.x) + xCenterOffset, panel.transform.position.y, 0);

            buttonObject.GetComponent<Image>().sprite = ability.Sprite;
        }
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void OnButtonClicked(AttackScriptableObject ability)
    {
        if (returnAction != null) returnAction.Invoke(ability);
        Destroy(gameObject);
    }

    public void SetReturnAction(UnityAction<AttackScriptableObject> action)
    {
        returnAction = action;
    }
}
