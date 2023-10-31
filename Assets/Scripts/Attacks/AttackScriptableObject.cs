using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class AttackScriptableObject : ScriptableObject
{
    [SerializeField] private AttackIndicator attackIndicator;
    [SerializeField] private AttackBehaviour attackBehaviour;
    [SerializeField] private Sprite sprite;
    [SerializeField] private AttackScriptableObject nextUpgrade;
    [SerializeField] private float nextUpgradePrice;
    [SerializeField] private bool cannotCastOnEnemy;

    [SerializeField] private float healthCost;
    [SerializeField] private float manaCost;
    [SerializeField] private float actionCost;
    [SerializeField] private float damage;
    [SerializeField] private float cooldownTime;

    public AttackIndicator AttackIndicator { get { return attackIndicator; } }
    public AttackBehaviour AttackBehaviour { get {  return attackBehaviour; } }
    public Sprite Sprite { get { return sprite; } }
    public AttackScriptableObject NextUpgrade { get { return nextUpgrade; } }
    public float NextUpgradePrice { get { return nextUpgradePrice; } }
    public bool IsCannotCastOnEnemy {  get { return cannotCastOnEnemy; } }
    public float HealthCost {get {return healthCost;}}
    public float ManaCost {get {return manaCost;}}
    public float ActionCost {get {return actionCost;}}
    public float Damage { get { return damage; } }
    public float CooldownTime { get {  return cooldownTime; } }
}
