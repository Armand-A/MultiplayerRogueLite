using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Health PlayerHealth;
    public Mana PlayerMana;
    public Action PlayerAction; // Stamina
    public Transform PlayerTransform;

    [Header("Data display objects")]
    [Tooltip("Action gauge script from object")]
    public HealthGauge _healthGauge;
    public ActionGauge _actionGauge;

    private int _uniqueID;

    private void Update()
    {
        _healthGauge.UpdateValue(PlayerHealth.Value, PlayerHealth.TotalValue);
        _actionGauge.UpdateValue(PlayerAction.Value, PlayerAction.TotalValue);
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
