using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : MonoBehaviour
{
    public ResourceManager ResourceMan;
    public StatManager StatMan;
    protected Transform EntityTransform;

    [Header("Data display objects")]
    [Tooltip("Health gauge script from object")]
    public HealthGauge _healthGauge;
    [Tooltip("Action gauge script from object")]
    public ActionGauge _actionGauge;

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
        ResourceMan = this.GetComponent<ResourceManager>();
        StatMan = this.GetComponent<StatManager>();

        ResourceMan.Initialize();
        EntityTransform = this.transform;

        _combatModeTimeout = new CooldownTimer(CombatTimeout);
    }
    
    protected virtual void OnEnable()
    {
        _combatModeTimeout.TimerCompleteEvent += ExitCombatMode;
    }

    protected virtual void OnDisable()
    {
        _combatModeTimeout.TimerCompleteEvent -= ExitCombatMode;
    }
    
    protected virtual void Update()
    {
        //if (_combatModeTimeout.IsActive)
        _combatModeTimeout.Update(Time.deltaTime);

        _healthGauge.UpdateValue(ResourceMan.Health.Value, ResourceMan.Health.TotalValue);
        _actionGauge.UpdateValue(ResourceMan.Action.Value, ResourceMan.Action.TotalValue);
        if (ResourceMan.Health.Value < 0.001f) DeathSequence();
    }

    public bool UpdateAction(float value)
    {
        //return false;
        return ResourceMan.Action.Remove(value);
    }

    public bool Damage(float value)
    {
        EnterCombatMode();
        return ResourceMan.Health.Remove(value);
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
