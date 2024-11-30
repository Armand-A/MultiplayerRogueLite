using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetedAbility)), AddComponentMenu("Ability/Behaviours/Damage/Damage Target")]
public class AbDamageTarget : MonoBehaviour
{
    TargetedAbility ability;

    private void Start()
    {
        ability = GetComponent<TargetedAbility>();

        EntityData targetEntity = ability.TargetObject.GetComponent<EntityData>();
        ability.DealDamageToEntity(targetEntity);
    }
}
