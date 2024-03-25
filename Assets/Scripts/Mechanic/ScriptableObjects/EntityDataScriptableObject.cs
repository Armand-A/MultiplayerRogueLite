using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EntityDataEnums;
using static Ability;

[CreateAssetMenu(fileName = "New Entity Data", menuName = "Entity Data")]
public class EntityDataScriptableObject : ScriptableObject
{
    [SerializeField]
    StatContainer[] stats = new StatContainer[]
    {
        new StatContainer(StatsEnum.HP, 100, 10),
        new StatContainer(StatsEnum.AP, 30, 10),
        new StatContainer(StatsEnum.HPRegen, 1, 1),
        new StatContainer(StatsEnum.APRegen, 1, 1),
        new StatContainer(StatsEnum.CDReduction, 0),
        new StatContainer(StatsEnum.Accuracy, 10),
        new StatContainer(StatsEnum.Evasiveness, 10),
        new StatContainer(StatsEnum.Crit_Dmg, 200, 100),
        new StatContainer(StatsEnum.Crit_Rate, 1),
        new StatContainer(StatsEnum.Speed, 250, 250, 5000),
        new StatContainer(StatsEnum.Luck, 10),
    };

    [SerializeField]
    ElementalContainer[] eStats = new ElementalContainer[]
    {
        new ElementalContainer(StatsEnum.Attack, 10),
        new ElementalContainer(StatsEnum.Defence, 10)
    };
/*

    public float[] HP { get { return new float[]{ _hp, _minHp, _maxHp}; } }
    public float[] AP { get { return new float[] { _ap, _minAp, _maxAp }; } }

    public float[] HPRegen { get { return new float[] { _hpRegen, _minHpRegen, _maxHpRegen }; } }
    public float[] APRegen { get { return new float[] { _apRegen, _minApRegen, _maxApRegen }; } }

    public float[] Attack { get { return _attack; } }
    public float[] MinMaxAttack { get { return new float[] {_minAttack, _maxAttack}; } }

    public float[] Defence { get { return _defence; } }
    public float[] MinMaxDefence { get { return new float[] {_minDefence, _maxDefence}; } }

    public float[] CDReduction { get { return new float[] { _cdReduction, _min_cdReduction, _max_cdReduction }; } }
    public float[] Accuracy { get { return new float[] { _accuracy, _minAccuracy, _maxAccuracy }; } }
    public float[] Evasiveness { get { return new float[] { _evasiveness, _minEvasiveness, _maxEvasiveness }; } }
    public float[] Crit_Dmg { get {  return new float[] { _crit_dmg, _minCrit_dmg, _maxCrit_dmg }; } }
    public float[] Crit_Rate { get {  return new float[] { _crit_rate, _minCrit_rate, _maxCrit_rate }; } }
    public float[] Speed { get { return new float[] { _speed, _minSpeed, _maxSpeed }; } }

    public float[] Luck { get { return new float[] { _luck, _minLuck, _maxLuck }; } }*/

    public StatContainer[] GetStats { get { return stats; } }
    public ElementalContainer[] GetEStats { get { return eStats; } }

    //public float[] InvincibilityTF { get { return new float[] { _invicibilityTF, _minInvicibilityTF, _maxInvicibilityTF }; } }

    [System.Serializable]
    public class StatContainer
    {
        public StatsEnum StatType;
        
        public float Value;
        public float Min;
        public float Max;

        public StatContainer(StatsEnum statType, float value = 0, float min = 0, float max = 999999999)
        {
            StatType = statType;

            Value = value;
            Min = min;
            Max = max;
        }
    }

    [System.Serializable]
    public class ElementalContainer
    {
        public StatsEnum StatType;
        public ElementalStats[] eStats = new ElementalStats[Enum.GetNames(typeof(ElementTypesEnum)).Length];

        public ElementalContainer(StatsEnum statType, float value = 0, float min = 0, float max = 999999999)
        {
            StatType = statType;

            for (int j = 0; j < DataEnumNames.ElementNames.Count; j++)
            {
                eStats[j] = new ElementalStats((ElementTypesEnum)j, value, min, max);
            }
        }
    }

    [System.Serializable]
    public class ElementalStats
    {
        public ElementTypesEnum elementType;

        public float Value;
        public float Min;
        public float Max;

        public ElementalStats(ElementTypesEnum eType, float value = 0, float min = 0, float max = 999999999)
        {
            elementType = eType;

            Value = value;
            Min = min;
            Max = max;
        }
    }
}
