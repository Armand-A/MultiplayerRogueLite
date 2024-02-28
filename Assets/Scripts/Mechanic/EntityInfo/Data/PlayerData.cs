using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerData : EntityData
{
    [Tooltip("Currency text")]
    public TMP_Text CurrencyText;

    protected override void Awake()
    {
        base.Awake();
        ResourceMan.AddCurrency();
        ResourceMan.HPRegenIsActive = true;
    }

    public void UpdateCurrencyText()
    {
        CurrencyText.text = (string)ResourceMan.Currency.ToString();
    }

    protected override void Update()
    {
        base.Update();
        UpdateCurrencyText();
    }

    public void PreviewActionCost(bool check, float cost = 0)
    {
        if (check)
        {
            _actionGauge.CostPreview(true, cost);
        }
        else
        {
            _actionGauge.CostPreview(false);
        }
    }
}
