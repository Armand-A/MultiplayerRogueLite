using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Health PlayerHealth;
    public Mana PlayerMana;
    public Action PlayerAction; // Stamina

    [Header("Data display objects")]
    [Tooltip("Action gauge script from object")]
    public ActionGauge ActionGaugeObject;

    private int _uniqueID;

    private void Update()
    {
        ActionGaugeObject.UpdateValue(PlayerAction.Value, PlayerAction.TotalValue);
    }
}
