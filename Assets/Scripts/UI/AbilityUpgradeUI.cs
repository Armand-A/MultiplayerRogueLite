using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityUpgradeUI : AbilityUI
{
    [SerializeField] Image beforeIcon;
    [SerializeField] Image afterIcon;
    [SerializeField] TextMeshProUGUI healthCostText;
    [SerializeField] TextMeshProUGUI manaCostText;
    [SerializeField] TextMeshProUGUI actionCostText;
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] Color normalTextColor;
    [SerializeField] Color goodTextColor;
    [SerializeField] Color badTextColor;

    public AttackScriptableObject ability;
    public UnityAction confirmAction;

    private void OnEnable()
    {
        if (ability != null) UpdateUI(ability);
    }

    public void Initialize(AttackScriptableObject ability, UnityAction confirmAction)
    {
        this.ability = ability;
        this.confirmAction = confirmAction;
        UpdateUI(ability);
    }

    public void OnCancel()
    {
        uiManager.CloseUI();
    }

    public void OnConfirm()
    {
        if (confirmAction != null) confirmAction.Invoke();
        uiManager.CloseUI();
    }

    private void UpdateUI(AttackScriptableObject ability)
    {
        beforeIcon.sprite = ability.Sprite;
        afterIcon.sprite = ability.NextUpgrade.Sprite;

        float healthDiff = ability.NextUpgrade.HealthCost - ability.HealthCost;
        healthCostText.text = string.Format("Health cost: {0} => {1} ({2}{3})", ability.HealthCost, ability.NextUpgrade.HealthCost, healthDiff > 0 ? "+" : "", healthDiff);
        healthCostText.color = healthDiff < 0.001 && healthDiff > -0.001 ? normalTextColor : healthDiff < 0 ? goodTextColor : badTextColor;

        float manaDiff = ability.NextUpgrade.ManaCost - ability.ManaCost;
        manaCostText.text = string.Format("Mana cost: {0} => {1} ({2}{3})", ability.ManaCost, ability.NextUpgrade.ManaCost, manaDiff > 0 ? "+" : "", manaDiff);
        manaCostText.color = manaDiff < 0.001 && manaDiff > -0.001 ? normalTextColor : manaDiff < 0 ? goodTextColor : badTextColor;

        float actionDiff = ability.NextUpgrade.ActionCost - ability.ActionCost;
        actionCostText.text = string.Format("Action cost: {0} => {1} ({2}{3})", ability.ActionCost, ability.NextUpgrade.ActionCost, actionDiff > 0 ? "+" : "", actionDiff);
        actionCostText.color = actionDiff < 0.001 && actionDiff > -0.001 ? normalTextColor : actionDiff < 0 ? goodTextColor : badTextColor;

        float damageDiff = ability.NextUpgrade.Damage - ability.Damage;
        damageText.text = string.Format("Damage: {0} => {1} ({2}{3})", ability.Damage, ability.NextUpgrade.Damage, damageDiff > 0 ? "+" : "", damageDiff);
        damageText.color = damageDiff < 0.001 && damageDiff > -0.001 ? normalTextColor : damageDiff > 0 ? goodTextColor : badTextColor;
    }
}
