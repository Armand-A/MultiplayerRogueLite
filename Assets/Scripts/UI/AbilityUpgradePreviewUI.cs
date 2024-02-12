using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityUpgradePreviewUI : AbilityUI
{
    [SerializeField] Image beforeIcon;
    [SerializeField] Image afterIcon;
    [SerializeField] TextMeshProUGUI healthCostText;
    [SerializeField] TextMeshProUGUI manaCostText;
    [SerializeField] TextMeshProUGUI actionCostText;
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] TextMeshProUGUI cooldownText;
    [SerializeField] TextMeshProUGUI errorText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Button confirmButton;
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
    }

    public void ShowError(string errorMsg)
    {
        errorText.text = errorMsg;
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

        float cooldownDiff = ability.NextUpgrade.CooldownTime - ability.CooldownTime;
        cooldownText.text = string.Format("Cooldown duration: {0} => {1} ({2}{3})", ability.CooldownTime, ability.NextUpgrade.CooldownTime, cooldownDiff > 0 ? "+" : "", cooldownDiff);
        cooldownText.color = cooldownDiff < 0.001 && cooldownDiff > -0.001 ? normalTextColor : cooldownDiff > 0 ? goodTextColor : badTextColor;

        errorText.text = null;

        Currency currencyObject = FindObjectOfType<ResourceManager>().Currency;
        if (currencyObject != null)
        {
            float currencyAfterPrice = currencyObject.Value - ability.NextUpgradePrice;
            priceText.text = string.Format("Price: ${0} (${1} => ${2})", ability.NextUpgradePrice, currencyObject.Value, currencyAfterPrice);
            errorText.text = currencyAfterPrice < 0 ? "Insufficient currency" : null;
        } else
        {
            throw new System.Exception("Currency object not found in AbilityUpgradePreviewUI");
        }
    }
}