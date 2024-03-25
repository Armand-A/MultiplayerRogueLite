using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EntityDataEnums;

public class EntityData : MonoBehaviour
{
    public ResourceManager ResourceMan;
    public StatManager StatMan;
    protected Transform EntityTransform;

    [Header("Data display objects")]
    [Tooltip("Health gauge script from object")]
    public Gauge _healthGauge;
    [Tooltip("Action gauge script from object")]
    public Gauge _actionGauge;

    [Header("Player Status")]
    [Tooltip("Checks if entity is in combat")]
    public bool CombatMode = false;
    [Tooltip("How long until entity can exit combat mode")]
    public float CombatTimeout = 3;
    [Tooltip("Checks if entity is alive")]
    public bool Alive = true; //For when entity is not destroyed on death

    protected string _uniqueID;
    public string UniqueID { get {  return _uniqueID; } }

    protected CooldownTimer _combatModeTimeout;

    protected virtual void Awake()
    {
        if (ResourceMan == null)
            ResourceMan = GetComponent<ResourceManager>();
        if (StatMan == null)
            StatMan = GetComponent<StatManager>();

        StatMan.Initialize();
        ResourceMan.Initialize(StatMan.Stats[(int)StatsEnum.HP].Value, StatMan.Stats[(int)StatsEnum.AP].Value);
        
        EntityTransform = transform;
        _combatModeTimeout = new CooldownTimer(CombatTimeout);

        StatUpdateEvent();
    }
    
    protected virtual void OnEnable()
    {
        StatMan.StatUpdateEvent += StatUpdateEvent;
        _combatModeTimeout.TimerCompleteEvent += ExitCombatMode;
    }

    protected virtual void OnDisable()
    {
        StatMan.StatUpdateEvent -= StatUpdateEvent;
        _combatModeTimeout.TimerCompleteEvent -= ExitCombatMode;
    }
    
    protected virtual void Update()
    {
        if (_combatModeTimeout.IsActive)
            _combatModeTimeout.Update(Time.deltaTime);
        UpdateGauge();

        if (ResourceMan.Health.Value < 0.001f) DeathSequence();
    }

    protected virtual void UpdateGauge()
    {
        _healthGauge.UpdateValue(ResourceMan.Health.Value, ResourceMan.Health.TotalValue);
        _actionGauge.UpdateValue(ResourceMan.Action.Value, ResourceMan.Action.TotalValue);
    }

    public virtual void StatUpdateEvent()
    {
        float hp = StatMan.Stats[(int)StatsEnum.HP].Value;
        float hpRegen = StatMan.Stats[(int)StatsEnum.HPRegen].Value;

        float ap = StatMan.Stats[(int)StatsEnum.AP].Value;
        float apRegen = StatMan.Stats[(int)StatsEnum.APRegen].Value;

        ResourceMan.UpdateResourceStats(hp, ap, hpRegen, apRegen);
    }

    public bool UseAction(float value)
    {
        return ResourceMan.Action.Remove(value);
    }

    public bool Damage(float incomingAttack)
    {
        EnterCombatMode();
        return ResourceMan.Health.Remove(incomingAttack);
    }

    public bool Damage(EntityData entity, Ability ability)
    {
        float value = StatMan.DamageCalculation(entity.StatMan.Stats, ability, StatMan.Stats);
        bool result = ResourceMan.Health.Remove(value);

        if (!result)
            DeathSequence();
        return result;
    }

    protected virtual void DeathSequence()
    {
        // temp
        Destroy(gameObject);
    }

    public void EnterCombatMode()
    {
        if (!CombatMode)
        {
            CombatMode = true;
            CombatModeProtocol();
        }
        _combatModeTimeout.Start();
    }

    protected virtual void ExitCombatMode()
    {
        CombatMode = false;
        OutOfCombat();
    }

    protected virtual void CombatModeProtocol()
    {
        StatMan.InCombat(true);
    }

    protected virtual void OutOfCombat()
    {
        StatMan.InCombat(false);
    }
}
