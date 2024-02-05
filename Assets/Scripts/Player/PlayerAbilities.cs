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

    public bool ImbueAbility(Ability fromAbility, Ability toAbility)
    {
        if (fromAbility == null) return false;
        if (!abilities.Contains(fromAbility)) return false;
        if (fromAbility.AcquisitionType != Ability.EAcquisitionType.Base) return false;
        if (!fromAbility.ImbueOptions.Exists((imbueOption) => imbueOption.resultAbility == toAbility)) return false;

        // replace ability in inventory with imbued ability
        int index = abilities.IndexOf(fromAbility);
        abilities[index] = toAbility;
        
        // also replace in hotbar if equipped
        if (equippedAbilites.Contains(fromAbility))
        {
            int indexInEquipped = equippedAbilites.IndexOf(fromAbility);
            equippedAbilites[indexInEquipped] = toAbility;
            changeEquippedAbilityEvent.Raise();
        }

        return true;
    }

    public void EquipAbilityInSlot(Ability newAbility, HotbarAbilitySlot slot)
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
