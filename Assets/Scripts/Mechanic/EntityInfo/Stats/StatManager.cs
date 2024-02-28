using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] private EntityDataScriptableObject _initialData;

    public delegate void StatUpdateHandler();
    public event StatUpdateHandler StatUpdateEvent;

    bool _statUpdated = false;

    // Sample key Stat[(int)EntityDataTypes.ElementType.Fire + (int)EntityDataTypes.Stats.Attack]
    private Dictionary<int, StatTemplate> _stats = new Dictionary<int, StatTemplate>();
    public Dictionary<int, StatTemplate> Stats {  get { return _stats; } }

    List<EntityDataTypes.Stats> _statNames = Enum.GetValues(typeof(EntityDataTypes.Stats)).Cast<EntityDataTypes.Stats>().ToList();
    List<EntityDataTypes.ElementTypes> _elementNames = Enum.GetValues(typeof(EntityDataTypes.ElementTypes)).Cast<EntityDataTypes.ElementTypes>().ToList();

    protected void Awake()
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
        for (int i = 0; i < _statNames.Count; i++)
        {
            if (_statNames[i] == EntityDataTypes.Stats.Attack || _statNames[i] == EntityDataTypes.Stats.Defence)
            {
                Debug.Log(_statNames[i]);
                for (int j = 0; j < _elementNames.Count; j++)
                {
                    Stats[(int)_statNames[i] + (int)_elementNames[j]] = new StatTemplate(gameObject);
                }
                continue;
            }
            Debug.Log(_statNames[i]);
            Stats[(int)_statNames[i]] = new StatTemplate(gameObject);
        }
    }

    /*
     Initialize entity stats from StatScriptableObject(baseStatObject)
     */
    public void InitializeStats()
    {
        for (int i = 0; i < _statNames.Count; i++)
        {
            //Debug.Log("Stat \"" + _statNames[i] + "\" added");
            switch (_statNames[i])
            {
                case EntityDataTypes.Stats.HP:
                    Stats[(int)_statNames[i]] = new StatTemplate(gameObject, _initialData.HP[0], _initialData.HP[1], _initialData.HP[2]);
                    break;
                case EntityDataTypes.Stats.AP:
                    Stats[(int)_statNames[i]] = new StatTemplate(gameObject, _initialData.AP[0], _initialData.AP[1], _initialData.AP[2]);
                    break;
                case EntityDataTypes.Stats.HPRegen:
                    Stats[(int)_statNames[i]] = new StatTemplate(gameObject, _initialData.AP[0], _initialData.AP[1], _initialData.AP[2]);
                    break;
                case EntityDataTypes.Stats.APRegen:
                    Stats[(int)_statNames[i]] = new StatTemplate(gameObject, _initialData.AP[0], _initialData.AP[1], _initialData.AP[2]);
                    break;
                case EntityDataTypes.Stats.Attack:
                    for (int j = 0; j < _elementNames.Count; j++)
                    {
                        Stats[(int)_statNames[i] + (int)_elementNames[j]] = new StatTemplate(gameObject, _initialData.Attack[j], _initialData.MinMaxAttack[0], _initialData.MinMaxAttack[1]);
                    }
                    break;
                case EntityDataTypes.Stats.Defence:
                    for (int j = 0; j < _elementNames.Count; j++)
                    {
                        Stats[(int)_statNames[i] + (int)_elementNames[j]] = new StatTemplate(gameObject, _initialData.Defence[j], _initialData.MinMaxDefence[0], _initialData.MinMaxDefence[1]);
                    }
                    break;
                case EntityDataTypes.Stats.Atk_Spd:
                    Stats[(int)_statNames[i]] = new StatTemplate(gameObject, _initialData.Atk_Spd[0], _initialData.Atk_Spd[1], _initialData.Atk_Spd[2]);
                    break;
                case EntityDataTypes.Stats.Accuracy:
                    Stats[(int)_statNames[i]] = new StatTemplate(gameObject, _initialData.Accuracy[0], _initialData.Accuracy[1], _initialData.Accuracy[2], statValueType:EntityDataTypes.ValueType.Percentage);
                    break;
                case EntityDataTypes.Stats.Evasiveness:
                    Stats[(int)_statNames[i]] = new StatTemplate(gameObject, _initialData.Evasiveness[0], _initialData.Evasiveness[1], _initialData.Evasiveness[2], statValueType: EntityDataTypes.ValueType.Percentage);
                    break;
                case EntityDataTypes.Stats.Crit_Dmg:
                    Stats[(int)_statNames[i]] = new StatTemplate(gameObject, _initialData.Crit_Dmg[0], _initialData.Crit_Dmg[1], _initialData.Crit_Dmg[2], statValueType: EntityDataTypes.ValueType.Percentage);
                    break;
                case EntityDataTypes.Stats.Crit_Rate:
                    Stats[(int)_statNames[i]] = new StatTemplate(gameObject, _initialData.Crit_Rate[0], _initialData.Crit_Rate[1], _initialData.Crit_Rate[2], statValueType: EntityDataTypes.ValueType.Percentage);
                    break;
                case EntityDataTypes.Stats.Speed:
                    Stats[(int)_statNames[i]] = new StatTemplate(gameObject, _initialData.Speed[0], _initialData.Speed[1], _initialData.Speed[2]);
                    break;
                case EntityDataTypes.Stats.Luck:
                    Stats[(int)_statNames[i]] = new StatTemplate(gameObject, _initialData.Speed[0], _initialData.Speed[1], _initialData.Speed[2]);
                    break;
                case EntityDataTypes.Stats.Invincibility_Time_Frame:
                    Stats[(int)_statNames[i]] = new StatTemplate(gameObject, _initialData.Speed[0], _initialData.Speed[1], _initialData.Speed[2]);
                    break;
                default:
                    Debug.LogError("Stat is not implemented properly");
                    break;
            }
        }
    }

    public void StatUpdate(EntityDataTypes.Stats s, EntityDataTypes.ElementTypes e, float flat, float percent, float multi)
    {
        Stats[(int)s + (int)e].StatUpdate(flat, percent, multi);
        _statUpdated = true;
    }

    public float[] GetAttack()
    {
        float[] attack = new float[_elementNames.Count];
        for (int j = 0; j < _elementNames.Count; j++)
        {
            attack[j] = Stats[(int)EntityDataTypes.Stats.Attack + (int)_elementNames[j]].Value;
        }
        return attack;
    }

    public float[] GetDefence()
    {
        float[] defence = new float[_elementNames.Count];
        for (int j = 0; j < _elementNames.Count; j++)
        {
            defence[j] = Stats[(int)EntityDataTypes.Stats.Defence + (int)_elementNames[j]].Value;
        }
        return defence;
    }
}