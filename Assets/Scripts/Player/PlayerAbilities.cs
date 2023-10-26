using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    // stores all abilities the player start out with in the beginning of a run
    [SerializeField] List<AttackScriptableObject> initialAbilities = new List<AttackScriptableObject>();

    // tracks all unlocked abilities and upgrade states
    List<AttackScriptableObject> abilities = new List<AttackScriptableObject>();

    // tracks the 4 equipped abilities in each slot
    [SerializeField] List<AttackScriptableObject> equippedAbilites = new List<AttackScriptableObject>(4);
    [SerializeField] GameEvent changeEquippedAbilityEvent;

    private void Awake()
    {
        abilities = new List<AttackScriptableObject>(initialAbilities);
    }

    public List<AttackScriptableObject> InitialAbilities { get { return initialAbilities; } }
    public List<AttackScriptableObject> Abilities { get { return abilities;  } }
    public List<AttackScriptableObject> EquippedAbilities { get { return equippedAbilites; } }

    public void UpgradeAbility(AttackScriptableObject ability)
    {
        if (ability == null) return;
        if (!abilities.Contains(ability)) return;
        if (ability.NextUpgrade == null) return;

        int index = abilities.IndexOf(ability);
        abilities[index] = ability.NextUpgrade;
        
        if (equippedAbilites.Contains(ability))
        {
            int indexInEquipped = equippedAbilites.IndexOf(ability);
            equippedAbilites[indexInEquipped] = ability.NextUpgrade;
            changeEquippedAbilityEvent.Raise();
        }
    }

    public void EquipAbilityInSlot(AttackScriptableObject newAbility, AttackSlot slot)
    {
        equippedAbilites[(int)slot] = newAbility;
    }
}
