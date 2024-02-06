using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<EntityDataTypes.Resource, ResourceTemplate> _resourceDict;
    public Dictionary<EntityDataTypes.Resource, ResourceTemplate>  ResourceDict {  get { return _resourceDict; } }

    public ResourceTemplate Health { get{ return _resourceDict[EntityDataTypes.Resource.HP]; } }
    public ResourceTemplate Action { get{ return _resourceDict[EntityDataTypes.Resource.AP]; } }

    protected void Awake()
    {
        if (!_resourceDict.ContainsKey(EntityDataTypes.Resource.HP))
            _resourceDict[EntityDataTypes.Resource.HP] = new Health();
        if (!_resourceDict.ContainsKey(EntityDataTypes.Resource.AP))
            _resourceDict[EntityDataTypes.Resource.AP] = new Action();
    }

    public void UpdateResourceStats(float hp, float ap)
    {
        _resourceDict[EntityDataTypes.Resource.HP].TotalValue = hp;
        _resourceDict[EntityDataTypes.Resource.AP].TotalValue = ap;
    }

    public float GetValue(EntityDataTypes.Resource resource)
    {
        return _resourceDict[resource].Value;
    }

    public float GetTotalValue(EntityDataTypes.Resource resource)
    {
        return _resourceDict[resource].TotalValue;
    }
}
