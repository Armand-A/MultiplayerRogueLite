using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EntityDataEnums;

public class PlayerData : EntityData
{
    [Tooltip("Currency text")]
    public TMP_Text CurrencyText;
    protected PlayerMovement _playerMovement;

    protected override void Awake()
    {
        ResourceMan.AddCurrency();
        ResourceMan.HPRegenIsActive = true;
        _playerMovement = GetComponent<PlayerMovement>();
        base.Awake();
    }

    public void UpdateCurrencyText()
    {
        CurrencyText.text = (string)ResourceMan.UpgradeCurrency.ToString();
        //CurrencyText.text = (string)ResourceMan.AbilityCurrency.ToString();
    }

    protected override void Update()
    {
        base.Update();
        CurrencyText.text = (string)ResourceMan.UpgradeCurrency.ToString();
        //CurrencyText.text = (string)ResourceMan.AbilityCurrency.ToString();
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

    public override void StatUpdateEvent()
    {
        base.StatUpdateEvent();
        _playerMovement.BaseSpeed = StatMan.Stats[(int)StatsEnum.Speed].Value;
    }
}
