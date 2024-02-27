using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityCard : MonoBehaviour, IPointerClickHandler
{
    [Header("Main fields")]
    [SerializeField] Ability ability;
    [SerializeField] UnityEvent<Ability> onCardClicked = new UnityEvent<Ability>();

    [Header("UI references")]
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] Image logoImage;
    [SerializeField] TextMeshProUGUI descText;

    void OnValidate()
    {
        RefreshUI();
    }

    void OnEnable()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        if (ability == null)
        {
            titleText.text = null;
            logoImage.sprite = null;
            descText.text = null;
            return;
        }

        titleText.text = ability.AbilityName;
        logoImage.sprite = ability.Sprite;
        descText.text = ability.Description;
    }

    public void SetAbility(Ability newAbility)
    {
        ability = newAbility;
        RefreshUI();
    }

    public void RegisterOnClickListener(UnityAction<Ability> action)
    {
        onCardClicked.AddListener(action);
    }

    public void UnregisterOnClickListener(UnityAction<Ability> action)
    {
        onCardClicked.RemoveListener(action);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        onCardClicked.Invoke(ability);
    }
}
