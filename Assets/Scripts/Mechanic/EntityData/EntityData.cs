using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : MonoBehaviour
{
    protected Health EntityHealth;
    //protected Mana EntityMana;
    protected Action EntityAction; // Stamina
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

    protected int _uniqueID;
    protected CooldownTimer _combatModeTimeout;
    

    protected virtual void Awake()
    {
        EntityHealth = GetComponent<Health>();
        //EntityMana = GetComponent<Mana>();
        EntityAction = GetComponent<Action>();
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
        if (_combatModeTimeout.IsActive)
            _combatModeTimeout.Update(Time.deltaTime);

        _healthGauge.UpdateValue(EntityHealth.Value, EntityHealth.TotalValue);
        _actionGauge.UpdateValue(EntityAction.Value, EntityAction.TotalValue);
        if (EntityHealth.Value < 0.001f) DeathSequence();
    }

    public bool UpdateAction(float actionCost)
    {
        return EntityAction.UpdateValue(actionCost);
    }

    public bool UpdateHealth(float deltaHealth)
    {
        EnterCombatMode();
        return EntityHealth.UpdateValue(deltaHealth);
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
        EntityHealth.InCombat(true);
        EntityAction.InCombat(true);
    }

    protected virtual void OutOfCombat()
    {
        EntityHealth.InCombat(false);
        EntityAction.InCombat(false);
    }
}
