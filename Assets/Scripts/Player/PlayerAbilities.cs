using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    // stores all abilities the player start out with in the beginning of a run
    [SerializeField] List<Ability> initialAbilities = new List<Ability>();

    // tracks all unlocked abilities and upgrade states
    List<Ability> abilities = new List<Ability>();

    // tracks the 4 equipped abilities in each slot
    [SerializeField] List<Ability> equippedAbilites = new List<Ability>(4);
    [SerializeField] GameEvent changeEquippedAbilityEvent;
    
    List<CooldownTimer> equippedAbilitiesTimers = new List<CooldownTimer>();

    private void Awake()
    {
        abilities = new List<Ability>(initialAbilities);
        equippedAbilitiesTimers = new List<CooldownTimer>(equippedAbilites.Capacity);
        for (int i = 0; i < equippedAbilitiesTimers.Capacity; i++)
        {
            CooldownTimer timer = new CooldownTimer(0);
            timer.Start();
            timer.Pause();
            equippedAbilitiesTimers.Add(timer);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        foreach (CooldownTimer timer in equippedAbilitiesTimers)
        {
            if (timer == null) continue;
            timer.Update(Time.deltaTime);
        }
    }

    public List<Ability> InitialAbilities { get { return initialAbilities; } }
    public List<Ability> Abilities { get { return abilities;  } }
    public List<Ability> EquippedAbilities { get { return equippedAbilites; } }

    public bool UpgradeAbility(Ability ability)
    {
        if (ability == null) return false;
        if (!abilities.Contains(ability)) return false;
        if (ability.NextUpgrade == null) return false;

        if (!FindObjectOfType<Currency>().Transaction((int)-ability.NextUpgradePrice)) return false;

        int index = abilities.IndexOf(ability);
        abilities[index] = ability.NextUpgrade;
        
        if (equippedAbilites.Contains(ability))
        {
            int indexInEquipped = equippedAbilites.IndexOf(ability);
            equippedAbilites[indexInEquipped] = ability.NextUpgrade;
            changeEquippedAbilityEvent.Raise();
        }

        return true;
    }

    public void EquipAbilityInSlot(Ability newAbility, AttackSlot slot)
    {
        equippedAbilites[(int)slot] = newAbility;
    }

    public void StartAbilityCooldown(int index)
    {
        CooldownTimer timer = equippedAbilitiesTimers[index];
        timer.Start(equippedAbilites[index].CooldownTime);
    }

    public bool GetIsAbilityAvailable(int index)
    {
        if (equippedAbilitiesTimers[index] == null) return false;
        return equippedAbilitiesTimers[index].IsCompleted;
    }

    public float GetAbilityCooldownPercentage(int index)
    {
        if (equippedAbilitiesTimers[index] == null) return 1f;
        float percentage = equippedAbilitiesTimers[index].PercentElapsed;
        return float.IsNaN(percentage) ? 1f : percentage;
    }
}
