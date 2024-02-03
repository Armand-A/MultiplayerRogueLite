using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity Base Stats", menuName = "Entity Base Stats")]
public class StatScriptableObject : ScriptableObject
{
    [Header("Base stat values for various entities.\nMin/Max possible allowed value for each stat")]

    [Header("Health Point #")]
    //[Min(0f)]
    [SerializeField] private float _hp = 10;
    //[Min(0f)]
    [SerializeField] private float _minHp;
    //[Min(0f)]
    [SerializeField] private float _maxHp = 999999999;

    [Header("Action Point #")]
    //[Min(0f)]
    [SerializeField] private float _ap = 20;
    //[Min(0f)]
    [SerializeField] private float _minAp;
    //[Min(0f)]
    [SerializeField] private float _maxAp = 999999999;

    [Header("Attack #")]
    [Tooltip("0(Normal)\n1(Fire)\n2(Water)\n3(Earth)\n4(Air)")]
    [SerializeField] private float[] _attack = new float[Enum.GetNames(typeof(StatTypes.ElementTypes)).Length];
    //[Tooltip("0(Normal)\n1(Fire)\n2(Water)\n3(Earth)\n4(Air)")]
    //[SerializeField] private float[] _minAttack = new float[Enum.GetNames(typeof(StatTypes.ElementTypes)).Length];
    [SerializeField] private float _minAttack = 1;
    //[Tooltip("0(Normal)\n1(Fire)\n2(Water)\n3(Earth)\n4(Air)")]
    //[SerializeField] private float[] _maxAttack = new float[Enum.GetNames(typeof(StatTypes.ElementTypes)).Length];
    [SerializeField] private float _maxAttack = 999999999;

    [Header("Defence #")]
    [Tooltip("0(Normal)\n1(Fire)\n2(Water)\n3(Earth)\n4(Air)")]
    [SerializeField] private float[] _defence = new float[Enum.GetNames(typeof(StatTypes.ElementTypes)).Length];
    //[Tooltip("0(Normal)\n1(Fire)\n2(Water)\n3(Earth)\n4(Air)")]
    //[SerializeField] private float[] _minDefence = new float[Enum.GetNames(typeof(StatTypes.ElementTypes)).Length];
    [SerializeField] private float _minDefence = 1;
    //[Tooltip("0(Normal)\n1(Fire)\n2(Water)\n3(Earth)\n4(Air)")]
    //[SerializeField] private float[] _maxDefence = new float[Enum.GetNames(typeof(StatTypes.ElementTypes)).Length];
    [SerializeField] private float _maxDefence = 999999999;

    [Header("Attack Speed #")]
    [SerializeField] private float _atk_speed = 10;
    [SerializeField] private float _minAtk_speed = 1;
    [SerializeField] private float _maxAtk_speed = 999999999;

    [Header("Accuracy %")]
    [SerializeField] private float _accuracy = 100;
    [SerializeField] private float _minAccuracy = 0;
    [SerializeField] private float _maxAccuracy = 999999999;

    [Header("Evasiveness %")]
    [SerializeField] private float _evasiveness = 0;
    [SerializeField] private float _minEvasiveness = 0;
    [SerializeField] private float _maxEvasiveness = 999999999;

    [Header("Crit Damage %")]
    [SerializeField] private float _crit_dmg = 200;
    [SerializeField] private float _minCrit_dmg;
    [SerializeField] private float _maxCrit_dmg = 999999999;

    [Header("Crit Rate %")]
    [SerializeField] private float _crit_rate;
    [SerializeField] private float _minCrit_rate;
    [SerializeField] private float _maxCrit_rate = 999999999;

    [Header("Speed #")]
    [SerializeField] private float _speed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed = 999999999;

    public float[] HP { get { return new float[]{ _hp, _minHp, _maxHp}; } }
    public float[] AP { get { return new float[] { _ap, _minAp, _maxAp }; } }

    public float[] Attack { get { return _attack; } }
    /*public float[] MinAttack { get { return _minAttack; } }
    public float[] MaxAttack { get { return _maxAttack; } }*/
    public float[] MinMaxAttack { get { return new float[] {_minAttack, _maxAttack}; } }

    public float[] Defence { get { return _defence; } }
    /*public float[] MinDefence { get { return _minDefence; } }
    public float[] MaxDefence { get { return _maxDefence; } }*/
    public float[] MinMaxDefence { get { return new float[] {_minDefence, _maxDefence}; } }

    public float[] Atk_Spd { get { return new float[] { _atk_speed, _minAtk_speed, _maxAtk_speed }; } }
    public float[] Accuracy { get { return new float[] { _accuracy, _minAccuracy, _maxAccuracy }; } }
    public float[] Evasiveness { get { return new float[] { _evasiveness, _minEvasiveness, _maxEvasiveness }; } }
    public float[] Crit_Dmg { get {  return new float[] { _crit_dmg, _minCrit_dmg, _maxCrit_dmg }; } }
    public float[] Crit_Rate { get {  return new float[] { _crit_rate, _minCrit_rate, _maxCrit_rate }; } }
    public float[] Speed { get { return new float[] { _speed, _minSpeed, _maxSpeed }; } }
}
