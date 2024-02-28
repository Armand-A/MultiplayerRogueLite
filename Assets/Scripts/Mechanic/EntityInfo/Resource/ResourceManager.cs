using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    /*private Dictionary<EntityDataTypes.Resource, ResourceTemplate> _resourceDict = new Dictionary<EntityDataTypes.Resource, ResourceTemplate>();
    public Dictionary<EntityDataTypes.Resource, ResourceTemplate>  ResourceDict {  get { return _resourceDict; } }*/

    public Health Health { get{ return _health; } }
    public Action Action { get{ return _action; } }
    public Currency Currency { get{ return _currency; } }

    private Health _health;
    private Action _action;
    private Currency _currency;

    float _regenInterval = 0.1f;

    public bool HPRegenIsActive = false;
    public bool APRegenIsActive = true;
    private CooldownTimer _regenTimer;

    public void Initialize()
    {
        if (_health == null)
            _health = new Health();
        if (_action == null)
            _action = new Action();
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

    public void AddCurrency()
    {
        if (_currency == null)
            _currency = new Currency();
    }

    public void UpdateResourceStats(float hp, float ap, float hpRegen = 1, float apRegen = 1)
    {
        _health.UpdateResource(hp, hpRegen);
        _action.UpdateResource(ap, apRegen);
    }

    public float DamageCalculation(float[] attack, float[] defence)
    {
        float totalDamage = 0;
        for (int i = 0; i < attack.Length; i++)
        {
            totalDamage += 10 * attack[i] / (10 + defence[i]);
        }
        return totalDamage;
    }

    private void Regen()
    {
        Debug.Log("Regen");
        if (HPRegenIsActive)
            Health.Regen(_regenInterval);
        if (APRegenIsActive)
            Action.Regen(_regenInterval);
    }
}
