using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTemplate : MonoBehaviour
{
    [SerializeField] UpgradeLevelsScriptableObject _upgradeData;
    public List<EntityDataTypes.Stats> AffectedStats;

    float _value = 0;
    public float Value { get{return _value + _value * _duplicates;}}

    float _price = 0;
    public float Price { get { return _price; } }

    public int UpgradeID { get { return _upgradeData.UpgradeID; } }
    public bool AllowDuplicates { get { return _upgradeData.AllowDuplicates; } }

    int _duplicates = 0;
    int _upgradeLevel = 0;

    public bool LevelUp()
    {
        if (_upgradeData.UpgradeObjects.Count < _upgradeLevel)
        {
            _upgradeLevel++;
            return true;
        }
        else
            return false;
    }
}