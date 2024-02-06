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
        AddCurrencyType();
    }

    public void AddCurrencyType()
    {
        if (ResourceMan.ResourceDict[EntityDataTypes.Resource.Currency] == null)
            ResourceMan.ResourceDict[EntityDataTypes.Resource.Currency] = new ResourceTemplate();

    }

    public void UpdateCurrencyText()
    {
        CurrencyText.text = (string)ResourceMan.GetValue(EntityDataTypes.Resource.Currency).ToString();
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
