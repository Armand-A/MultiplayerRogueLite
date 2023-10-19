using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackScriptableObject : ScriptableObject
{
    [SerializeField] private AttackIndicator attackIndicator;
    [SerializeField] private AttackBehaviour attackBehaviour;

    public AttackIndicator AttackIndicator { get { return attackIndicator; } }
    public AttackBehaviour AttackBehaviour { get {  return attackBehaviour; } }
}
