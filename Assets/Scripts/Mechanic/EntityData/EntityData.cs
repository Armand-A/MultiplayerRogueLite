using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : MonoBehaviour
{
    protected Health EntityHealth;
    protected Mana EntityMana;
    protected Action EntityAction; // Stamina
    protected Transform EntityTransform;

    [Header("Data display objects")]
    [Tooltip("Action gauge script from object")]
    public HealthGauge _healthGauge;
    public ActionGauge _actionGauge;

    public bool Alive = true;

    protected int _uniqueID;

    protected virtual void Awake()
    {
        EntityHealth = GetComponent<Health>();
        EntityMana = GetComponent<Mana>();
        EntityAction = GetComponent<Action>();
        EntityTransform = this.transform;
    }
    
    protected virtual void Update()
    {
        _healthGauge.UpdateValue(EntityHealth.Value, EntityHealth.TotalValue);
        _actionGauge.UpdateValue(EntityAction.Value, EntityAction.TotalValue);
    }

    public bool UpdateAction(float actionCost)
    {
        return EntityAction.UpdateValue(actionCost);
    }

    public void DeathSequence()
    {

    }
}
