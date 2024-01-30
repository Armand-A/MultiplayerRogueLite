using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Base Stat template", menuName = "Entity Base Stats")]
public class EntityStatScriptableObject : ScriptableObject
{
    [SerializeField] private float _hp;
    [SerializeField] private float _ap;
    [SerializeField] private float _speed;
    [SerializeField] private float[] _attack = new float[Enum.GetNames(typeof(StatTypes.ElementTypes)).Length];
    [SerializeField] private float _atk_spd;
    [SerializeField] private float[] _defence = new float[Enum.GetNames(typeof(StatTypes.ElementTypes)).Length];
    [SerializeField] private float _accuracy;
    [SerializeField] private float _evasiveness;
    [SerializeField] private float _crit_rate;
    [SerializeField] private float _crit_atk;

    public float HP { get { return _hp; } }
    public float AP { get { return _ap; } }
    public float Speed { get { return _speed; } }
    public float[] Attack { get { return _attack; } }
    public float Atk_Spd { get { return _atk_spd; } }
    public float[] Defence { get { return _defence; } }
    public float Accuracy { get { return _accuracy; } }
    public float Evasiveness { get { return _evasiveness; } }
    public float Crit_rate { get {  return _crit_rate; } }
    public float Crit_atk { get {  return _crit_atk; } }
}
