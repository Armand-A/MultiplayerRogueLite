using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EntityDataEnums;
using static EntityDataScriptableObject;

public class StatManager : MonoBehaviour
{
    [SerializeField] private EntityDataScriptableObject _initialData;

    public delegate void StatUpdateHandler();
    public event StatUpdateHandler StatUpdateEvent;

    bool _statUpdated = false;

    // Sample key Stat[(int)EntityDataTypes.ElementType.Fire + (int)StatsEnum.Attack]
    private Dictionary<int, StatTemplate> _stats = new Dictionary<int, StatTemplate>();
    public Dictionary<int, StatTemplate> Stats {  get { return _stats; } }

    public void Initialize()
    {
        if (_initialData == null)
            StatDefault();
        else
            InitializeStats();

        _statUpdated = true;
    }

    private void Update()
    {
        if (_statUpdated) 
        {
            StatUpdateEvent?.Invoke();
            _statUpdated = false;
        }
    }

    public void InCombat(bool combatMode)
    {
        if (combatMode)
        {
            
        }
    }

    /*
     * Initialize stat with all stats as 10 
    */
    public void StatDefault()
    {
        for (int i = 0; i < DataEnumNames.StatNames.Count; i++)
        {
            if (DataEnumNames.StatNames[i] == StatsEnum.Attack || DataEnumNames.StatNames[i] == StatsEnum.Defence)
            {
                Debug.Log(DataEnumNames.StatNames[i]);
                for (int j = 0; j < DataEnumNames.ElementNames.Count; j++)
                {
                    Stats[(int)DataEnumNames.StatNames[i] + (int)DataEnumNames.ElementNames[j]] = new StatTemplate(gameObject);
                }
                continue;
            }
            Debug.Log(DataEnumNames.StatNames[i]);
            Stats[(int)DataEnumNames.StatNames[i]] = new StatTemplate(gameObject);
        }
    }

    /*
     Initialize entity stats from StatScriptableObject(baseStatObject)
     */
    public void InitializeStats()
    {
        StatContainer[] stats = _initialData.GetStats;
        ElementalContainer[] eStats = _initialData.GetEStats;
        for (int i = 0; i < stats.Length; i++)
        {
            Stats[(int)stats[i].StatType] = new StatTemplate(gameObject, stats[i].Value, stats[i].Min, stats[i].Max);
        }
        for (int i = 0; i < eStats.Length; i++)
        {
            for (int j = 0; j < DataEnumNames.ElementNames.Count; j++)
            {
                Stats[(int)eStats[i].StatType + (int)eStats[i].eStats[j].elementType] = new StatTemplate(gameObject, eStats[i].eStats[j].Value, eStats[i].eStats[j].Min, eStats[i].eStats[j].Max);
            }
        }
    }

    public void StatUpdate(StatsEnum s, ElementTypesEnum e, float flat, float percent)
    {
        Stats[(int)s + (int)e].StatModifier(flat, percent);
        _statUpdated = true;
    }

    public float[] GetAttack()
    {
        float[] attack = new float[DataEnumNames.ElementNames.Count];
        for (int j = 0; j < DataEnumNames.ElementNames.Count; j++)
        {
            attack[j] = Stats[(int)StatsEnum.Attack + (int)DataEnumNames.ElementNames[j]].Value;
        }
        return attack;
    }

    public float[] GetDefence()
    {
        float[] defence = new float[DataEnumNames.ElementNames.Count];
        for (int j = 0; j < DataEnumNames.ElementNames.Count; j++)
        {
            defence[j] = Stats[(int)StatsEnum.Defence + (int)DataEnumNames.ElementNames[j]].Value;
        }
        return defence;
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

    /// <summary>
    /// Damange calculation using
    /// Crit rate, Crit Damage, Attack, Defence
    /// </summary>
    /// <param name="enemyStats"></param>
    /// <param name="ability"></param>
    /// <param name="entityStats"></param>
    /// <returns></returns>
    public float DamageCalculation(Dictionary<int, StatTemplate> enemyStats, Ability ability, Dictionary<int, StatTemplate> entityStats)
    {
        float totalDamage = 0;
        float critrate = enemyStats[(int)StatsEnum.Crit_Rate].Value;
        float randcrit = UnityEngine.Random.Range(0, 100);

        for (int i = 0; i < Enum.GetNames(typeof(ElementTypesEnum)).Length; i++)
        {
            float attack = randcrit <= critrate ? enemyStats[(int)StatsEnum.Attack + i].Value * (enemyStats[(int)StatsEnum.Crit_Dmg].Value/100)
            : enemyStats[(int)StatsEnum.Attack + i].Value;

            totalDamage += 10 * attack / (10 + enemyStats[(int)StatsEnum.Defence + i].Value);
        }
        return totalDamage;
    }
}