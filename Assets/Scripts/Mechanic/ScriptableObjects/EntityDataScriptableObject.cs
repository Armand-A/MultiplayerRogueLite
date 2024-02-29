using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity Data", menuName = "Entity Data")]
public class EntityDataScriptableObject : ScriptableObject
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

    [Header("Health Point Recovery Rate per second")]
    [SerializeField] private float _hpRegen = 2;
    [SerializeField] private float _minHpRegen;
    [SerializeField] private float _maxHpRegen = 999999999;

    [Header("Action Point Recovery Rate per second")]
    [SerializeField] private float _apRegen = 2;
    [SerializeField] private float _minApRegen;
    [SerializeField] private float _maxApRegen = 999999999;

    [Header("Attack #")]
    [Tooltip("0(Normal)\n1(Fire)\n2(Water)\n3(Earth)\n4(Air)")]
    [SerializeField] private float[] _attack = new float[Enum.GetNames(typeof(EntityDataTypes.ElementTypes)).Length];
    [SerializeField] private float _minAttack = 0;
    [SerializeField] private float _maxAttack = 999999999;

    [Header("Defence #")]
    [Tooltip("0(Normal)\n1(Fire)\n2(Water)\n3(Earth)\n4(Air)")]
    [SerializeField] private float[] _defence = new float[Enum.GetNames(typeof(EntityDataTypes.ElementTypes)).Length];
    [SerializeField] private float _minDefence = 0;
    [SerializeField] private float _maxDefence = 999999999;

    [Header("Attack Speed #")]
    [SerializeField] private float _cdReduction = 10;
    [SerializeField] private float _min_cdReduction = 1;
    [SerializeField] private float _max_cdReduction = 999999999;

    [Header("Accuracy %")]
    [SerializeField] private float _accuracy = 1.0f;
    [SerializeField] private float _minAccuracy = 0;
    [SerializeField] private float _maxAccuracy = 999999999;

    [Header("Evasiveness %")]
    [SerializeField] private float _evasiveness = 0;
    [SerializeField] private float _minEvasiveness = 0;
    [SerializeField] private float _maxEvasiveness = 999999999;

    [Header("Crit Damage %")]
    [SerializeField] private float _crit_dmg = 2.0f;
    [SerializeField] private float _minCrit_dmg = 0;
    [SerializeField] private float _maxCrit_dmg = 999999999;

    [Header("Crit Rate %")]
    [SerializeField] private float _crit_rate = 0.01f;
    [SerializeField] private float _minCrit_rate = 0;
    [SerializeField] private float _maxCrit_rate = 999999999;

    [Header("Speed #")]
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _minSpeed = 0;
    [SerializeField] private float _maxSpeed = 999999999;

    [Header("Luck %")]
    [SerializeField] private float _luck = 0;
    [SerializeField] private float _minLuck = 0;
    [SerializeField] private float _maxLuck = 999999999;

    [Header("Invincibility_Time_Frame #")]
    [SerializeField] private float _invicibilityTF;
    [SerializeField] private float _minInvicibilityTF = 0;
    [SerializeField] private float _maxInvicibilityTF = 999999999;

    public float[] HP { get { return new float[]{ _hp, _minHp, _maxHp}; } }
    public float[] AP { get { return new float[] { _ap, _minAp, _maxAp }; } }

    public float[] HPRegen { get { return new float[] { _hpRegen, _minHpRegen, _maxHpRegen }; } }
    public float[] APRegen { get { return new float[] { _apRegen, _minApRegen, _maxApRegen }; } }

    public float[] Attack { get { return _attack; } }
    public float[] MinMaxAttack { get { return new float[] {_minAttack, _maxAttack}; } }

    public float[] Defence { get { return _defence; } }
    public float[] MinMaxDefence { get { return new float[] {_minDefence, _maxDefence}; } }

    public float[] Atk_Spd { get { return new float[] { _cdReduction, _min_cdReduction, _max_cdReduction }; } }
    public float[] Accuracy { get { return new float[] { _accuracy, _minAccuracy, _maxAccuracy }; } }
    public float[] Evasiveness { get { return new float[] { _evasiveness, _minEvasiveness, _maxEvasiveness }; } }
    public float[] Crit_Dmg { get {  return new float[] { _crit_dmg, _minCrit_dmg, _maxCrit_dmg }; } }
    public float[] Crit_Rate { get {  return new float[] { _crit_rate, _minCrit_rate, _maxCrit_rate }; } }
    public float[] Speed { get { return new float[] { _speed, _minSpeed, _maxSpeed }; } }

    public float[] Luck { get { return new float[] { _luck, _minLuck, _maxLuck }; } }
    public float[] InvincibilityTF { get { return new float[] { _invicibilityTF, _minInvicibilityTF, _maxInvicibilityTF }; } }
}
