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

    public void Initialize()
    {
        if (_health == null)
            _health = new Health();
        if (_action == null)
            _action = new Action();
    }

    public void AddCurrency()
    {
        if (_currency == null)
            _currency = new Currency();
    }

    public void UpdateResourceStats(float hp, float ap)
    {
        _health.TotalValue = hp;
        _action.TotalValue = ap;
    }
/*
    public float GetValue(EntityDataTypes.Resource resource)
    {
        return _resourceDict[resource].Value;
    }

    public float GetTotalValue(EntityDataTypes.Resource resource)
    {
        return _resourceDict[resource].TotalValue;
    }*/
}
