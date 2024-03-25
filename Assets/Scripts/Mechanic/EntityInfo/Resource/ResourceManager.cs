using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public Health Health { get{ return _health; } }
    public Action Action { get{ return _action; } }
    public Currency UpgradeCurrency { get{ return _upgrade_currency; } }
    public Currency AbilityCurrency { get{ return _ability_currency; } }

    private Health _health;
    private Action _action;
    private Currency _upgrade_currency;
    private Currency _ability_currency;

    float _regenInterval = 0.1f;

    public bool HPRegenIsActive = false;
    public bool APRegenIsActive = true;
    private CooldownTimer _regenTimer;

    public void Initialize(float health, float action)
    {
        if (_health == null)
            _health = new Health(health);
        if (_action == null)
            _action = new Action(action);
    }

    private void Awake()
    {
        _regenTimer = new CooldownTimer(_regenInterval, true);
        _regenTimer.Start();
    }

    private void OnEnable()
    {
        _regenTimer.TimerCompleteEvent += Regen;
    }

    private void OnDisable()
    {
        _regenTimer.TimerCompleteEvent -= Regen;
    }

    private void FixedUpdate()
    {       
        if (HPRegenIsActive || APRegenIsActive)
        {
            _regenTimer.Update(Time.deltaTime);
        }
    }

    public void AddCurrency(float value = 100)
    {
        if (_upgrade_currency == null)
            _upgrade_currency = new Currency(value);

        if (_ability_currency == null)
            _ability_currency = new Currency(value);
    }

    public void UpdateResourceStats(float hp, float ap, float hpRegen = 1, float apRegen = 1)
    {
        _health.UpdateResource(hp, hpRegen);
        _action.UpdateResource(ap, apRegen);
    }

    private void Regen()
    {
        if (HPRegenIsActive)
            Health.Regen(_regenInterval);
        if (APRegenIsActive)
            Action.Regen(_regenInterval);
    }
}