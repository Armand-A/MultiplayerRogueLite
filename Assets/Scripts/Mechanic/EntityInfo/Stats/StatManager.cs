using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] private EntityDataScriptableObject _initialData;

    // Sample key Stat[(int)EntityDataTypes.ElementType.Fire + (int)EntityDataTypes.Stats.Attack]
    private Dictionary<int, StatTemplate> Stat;
    // key = UpgradeID
    private List<UpgradeTemplate> Upgrades;

    List<EntityDataTypes.Stats> _statNames = Enum.GetValues(typeof(EntityDataTypes.Stats)).Cast<EntityDataTypes.Stats>().ToList();
    List<EntityDataTypes.ElementTypes> _elementNames = Enum.GetValues(typeof(EntityDataTypes.ElementTypes)).Cast<EntityDataTypes.ElementTypes>().ToList();

    protected void Awake()
    {
        if (_initialData == null)
            StatDefault();
        else
            InitializeStats();
    }

    public void InCombat(bool combatMode)
    {
        if (combatMode)
        {
            
        }
    }

    public void StatUpgrade(UpgradeTemplate upgrade)
    {
        Upgrades.Add(upgrade);
    }

    /*
     * Initialize stat with all stats as 10 
    */
    public void StatDefault()
    {
        for (int i = 0; i < _statNames.Count; i++)
        {
            if (_statNames[i] == EntityDataTypes.Stats.Attack || _statNames[i] == EntityDataTypes.Stats.Defence)
            {
                Debug.Log(_statNames[i]);
                for (int j = 0; j < _elementNames.Count; j++)
                {
                    Stat[(int)_statNames[i] + (int)_elementNames[j]] = new StatTemplate();
                }
                continue;
            }
            Debug.Log(_statNames[i]);
            Stat[(int)_statNames[i]] = new StatTemplate();
        }
    }

    /*
     Initialize entity stats from StatScriptableObject(baseStatObject)
     */
    public void InitializeStats()
    {
        for (int i = 0; i < _statNames.Count; i++)
        {
            Debug.Log("Stat \"" + _statNames[i] + "\" added");
            switch (_statNames[i])
            {
                case EntityDataTypes.Stats.HP:
                    Stat[(int)_statNames[i]] = new StatTemplate(_initialData.HP[0], _initialData.HP[1], _initialData.HP[2]);
                    break;
                case EntityDataTypes.Stats.AP:
                    Stat[(int)_statNames[i]] = new StatTemplate(_initialData.AP[0], _initialData.AP[1], _initialData.AP[2]);
                    break;
                case EntityDataTypes.Stats.Attack:
                    for (int j = 0; j < _elementNames.Count; j++)
                    {
                        Stat[(int)_statNames[i] + (int)_elementNames[j]] = new StatTemplate(_initialData.Attack[j], _initialData.MinMaxAttack[0], _initialData.MinMaxAttack[1]);
                    }
                    break;
                case EntityDataTypes.Stats.Defence:
                    for (int j = 0; j < _elementNames.Count; j++)
                    {
                        Stat[(int)_statNames[i] + (int)_elementNames[j]] = new StatTemplate(_initialData.Defence[j], _initialData.MinMaxDefence[0], _initialData.MinMaxDefence[1]);
                    }
                    break;
                case EntityDataTypes.Stats.Atk_Spd:
                    Stat[(int)_statNames[i]] = new StatTemplate(_initialData.Atk_Spd[0], _initialData.Atk_Spd[1], _initialData.Atk_Spd[2]);
                    break;
                case EntityDataTypes.Stats.Accuracy:
                    Stat[(int)_statNames[i]] = new StatTemplate(_initialData.Accuracy[0], _initialData.Accuracy[1], _initialData.Accuracy[2], statValueType:EntityDataTypes.ValueType.Percentage);
                    break;
                case EntityDataTypes.Stats.Evasiveness:
                    Stat[(int)_statNames[i]] = new StatTemplate(_initialData.Evasiveness[0], _initialData.Evasiveness[1], _initialData.Evasiveness[2], statValueType: EntityDataTypes.ValueType.Percentage);
                    break;
                case EntityDataTypes.Stats.Crit_Dmg:
                    Stat[(int)_statNames[i]] = new StatTemplate(_initialData.Crit_Dmg[0], _initialData.Crit_Dmg[1], _initialData.Crit_Dmg[2], statValueType: EntityDataTypes.ValueType.Percentage);
                    break;
                case EntityDataTypes.Stats.Crit_Rate:
                    Stat[(int)_statNames[i]] = new StatTemplate(_initialData.Crit_Rate[0], _initialData.Crit_Rate[1], _initialData.Crit_Rate[2], statValueType: EntityDataTypes.ValueType.Percentage);
                    break;
                case EntityDataTypes.Stats.Speed:
                    Stat[(int)_statNames[i]] = new StatTemplate(_initialData.Speed[0], _initialData.Speed[1], _initialData.Speed[2]);
                    break;
                default:
                    Debug.LogError("Stat is not implemented properly");
                    break;
            }
        }
    }
}