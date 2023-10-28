using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerData : EntityData
{   
    [Header("Player Currency")]
    private Currency _playerGold;
    [Tooltip("Value text")]
    public TMP_Text ValueText;

    protected override void Awake()
    {
        base.Awake();
        _playerGold = GetComponent<Currency>();
    }

    protected override void Update()
    {
        base.Update();
        ValueText.text = (string) _playerGold.Value.ToString();
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
