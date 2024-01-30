using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerData : EntityData
{   
    [Header("Player Currency")]
    private Currency _playerGold;
    [Tooltip("Currency text")]
    public TMP_Text CurrencyText;

    protected override void Awake()
    {
        base.Awake();
        _playerGold = GetComponent<Currency>();
    }

    protected override void Update()
    {
        base.Update();
        CurrencyText.text = (string) _playerGold.Value.ToString();
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
