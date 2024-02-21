using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityEquipUI : AbilityUI
{
    [SerializeField] List<GameObject> buttons;
    [SerializeField] AbilityLibraryUI abilityLibraryUI;
    [SerializeField] GameEvent equippedAbilityChangeEvent;

    PlayerAbilities playerAbilities;
    List<Ability> editingAbilities;
    AbilityLibraryUI abilitylibraryUiInstance;

    List<UnityAction<Ability>> activeListeners = new List<UnityAction<Ability>>();

    private void OnEnable()
    {
        playerAbilities = GameObject.FindObjectOfType<PlayerAbilities>();
        editingAbilities = new List<Ability>(playerAbilities.EquippedAbilities);

        foreach (HotbarAbilitySlot slot in Enum.GetValues(typeof(HotbarAbilitySlot)))
        {
            if (slot == HotbarAbilitySlot.None) continue;
            if (editingAbilities[(int)slot] != null)
            {
                buttons[(int)slot].GetComponent<Image>().sprite = editingAbilities[(int)slot].Sprite;
            }
            else
            {
                buttons[(int)slot].GetComponent<Image>().sprite = null;
            }
            buttons[(int)slot].GetComponent<Button>().onClick.RemoveAllListeners();
            buttons[(int)slot].GetComponent<Button>().onClick.AddListener(() =>
            {
                abilitylibraryUiInstance = (AbilityLibraryUI) uiManager.OpenUIAndGet(abilityLibraryUI);
                UnityAction<Ability> action = (newAbility) => OnNewAbilitySelected(newAbility, slot);
                abilitylibraryUiInstance.AddOnAbilityClickedListener(action);
                activeListeners.Add(action);
            });
        }
    }

    private void CommitChanges()
    {
        List<Ability> originalAbilities = playerAbilities.EquippedAbilities;
        foreach (HotbarAbilitySlot slot in Enum.GetValues(typeof(HotbarAbilitySlot)))
        {
            if (slot == HotbarAbilitySlot.None) continue;
            if (originalAbilities[(int)slot] == editingAbilities[(int)slot]) continue;
            playerAbilities.EquipAbilityInSlot(editingAbilities[(int)slot], slot);
            //hotbarIcons[(int)slot].GetComponent<Image>().sprite = newAbility.Sprite;
        }
        equippedAbilityChangeEvent.Raise();
    }

    public void OnNewAbilitySelected(Ability newAbility, HotbarAbilitySlot slot)
    {
        if (newAbility == null) return; // case: nothing selected
        if (newAbility == playerAbilities.EquippedAbilities[(int)slot]) return; // case: same ability selected, no change

        editingAbilities[(int)slot] = newAbility;
        buttons[(int)slot].GetComponent<Image>().sprite = newAbility.Sprite;

        // close ability library ui
        foreach (UnityAction<Ability> listener in activeListeners)
        {
            abilityLibraryUI.RemoveOnAbilityClickedListener(listener);
        }
        abilitylibraryUiInstance = null;
        uiManager.CloseUI();
    }

    public void OnConfirm()
    {
        CommitChanges();
        uiManager.CloseUI();
    }

    public void OnCancel()
    {
        uiManager.CloseUI();
    }
}