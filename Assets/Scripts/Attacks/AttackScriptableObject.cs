using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class AttackScriptableObject : ScriptableObject
{
    [SerializeField] private AttackIndicator attackIndicator;
    [SerializeField] private AttackBehaviour attackBehaviour;

    [SerializeField] private float healthCost;
    [SerializeField] private float manaCost;
    [SerializeField] private float actionCost;

    public AttackIndicator AttackIndicator { get { return attackIndicator; } }
    public AttackBehaviour AttackBehaviour { get {  return attackBehaviour; } }
    public float HealthCost {get {return healthCost;}}
    public float ManaCost {get {return manaCost;}}
    public float ActionCost {get {return actionCost;}}
}
