using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityStatManager : MonoBehaviour
{
    [SerializeField] private StatScriptableObject _baseStatObject;

    // Sample key
    private Dictionary<int, StatTemplate> Stat;
    private 

    protected void Awake()
    {
        if (_baseStatObject == null)
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

    /*
     * Initialize stat with all stats as 10 
    */
    public void StatDefault()
    {
        List<StatTypes.Stats> statsNames = Enum.GetValues(typeof(StatTypes.Stats)).Cast<StatTypes.Stats>().ToList();
        List<StatTypes.ElementTypes> elementNames = Enum.GetValues(typeof(StatTypes.ElementTypes)).Cast<StatTypes.ElementTypes>().ToList();

        for (int i = 0; i < statsNames.Count; i++)
        {
            if (statsNames[i] == StatTypes.Stats.Attack || statsNames[i] == StatTypes.Stats.Defence)
            {
                Debug.Log(statsNames[i]);
                for (int j = 0; j < elementNames.Count; j++)
                {
                    Stat[(int)statsNames[i] + (int)elementNames[j]] = new StatTemplate();
                }
                continue;
            }
            Debug.Log(statsNames[i]);
            Stat[(int)statsNames[i]] = new StatTemplate();
        }
    }

    /*
     Initialize entity stats from StatScriptableObject(baseStatObject)
     */
    public void InitializeStats()
    {
        List<StatTypes.Stats> statsNames = Enum.GetValues(typeof(StatTypes.Stats)).Cast<StatTypes.Stats>().ToList();
        List<StatTypes.ElementTypes> elementNames = Enum.GetValues(typeof(StatTypes.ElementTypes)).Cast<StatTypes.ElementTypes>().ToList();

        for (int i = 0; i < statsNames.Count; i++)
        {
            Debug.Log("Stat \"" + statsNames[i] + "\" added");
            switch (statsNames[i])
            {
                case StatTypes.Stats.HP:
                    Stat[(int)statsNames[i]] = new StatTemplate(_baseStatObject.HP[0], _baseStatObject.HP[1], _baseStatObject.HP[2], true);
                    break;
                case StatTypes.Stats.AP:
                    Stat[(int)statsNames[i]] = new StatTemplate(_baseStatObject.AP[0], _baseStatObject.AP[1], _baseStatObject.AP[2], true, true);
                    break;
                case StatTypes.Stats.Attack:
                    for (int j = 0; j < elementNames.Count; j++)
                    {
                        Stat[(int)statsNames[i] + (int)elementNames[j]] = new StatTemplate(_baseStatObject.Attack[j], _baseStatObject.MinMaxAttack[0], _baseStatObject.MinMaxAttack[1]);
                    }
                    break;
                case StatTypes.Stats.Defence:
                    for (int j = 0; j < elementNames.Count; j++)
                    {
                        Stat[(int)statsNames[i] + (int)elementNames[j]] = new StatTemplate(_baseStatObject.Defence[j], _baseStatObject.MinMaxDefence[0], _baseStatObject.MinMaxDefence[1]);
                    }
                    break;
                case StatTypes.Stats.Atk_Spd:
                    Stat[(int)statsNames[i]] = new StatTemplate(_baseStatObject.Atk_Spd[0], _baseStatObject.Atk_Spd[1], _baseStatObject.Atk_Spd[2]);
                    break;
                case StatTypes.Stats.Accuracy:
                    Stat[(int)statsNames[i]] = new StatTemplate(_baseStatObject.Accuracy[0], _baseStatObject.Accuracy[1], _baseStatObject.Accuracy[2], statValueType:StatTypes.ValueType.Percentage);
                    break;
                case StatTypes.Stats.Evasiveness:
                    Stat[(int)statsNames[i]] = new StatTemplate(_baseStatObject.Evasiveness[0], _baseStatObject.Evasiveness[1], _baseStatObject.Evasiveness[2], statValueType: StatTypes.ValueType.Percentage);
                    break;
                case StatTypes.Stats.Crit_Dmg:
                    Stat[(int)statsNames[i]] = new StatTemplate(_baseStatObject.Crit_Dmg[0], _baseStatObject.Crit_Dmg[1], _baseStatObject.Crit_Dmg[2], statValueType: StatTypes.ValueType.Percentage);
                    break;
                case StatTypes.Stats.Crit_Rate:
                    Stat[(int)statsNames[i]] = new StatTemplate(_baseStatObject.Crit_Rate[0], _baseStatObject.Crit_Rate[1], _baseStatObject.Crit_Rate[2], statValueType: StatTypes.ValueType.Percentage);
                    break;
                case StatTypes.Stats.Speed:
                    Stat[(int)statsNames[i]] = new StatTemplate(_baseStatObject.Speed[0], _baseStatObject.Speed[1], _baseStatObject.Speed[2]);
                    break;
                default:
                    Debug.LogError("Stat is not implemented properly");
                    break;
            }
        }
    }
}