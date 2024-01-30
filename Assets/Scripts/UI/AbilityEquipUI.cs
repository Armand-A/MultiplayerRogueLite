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

    private PlayerAbilities playerAbilities;
    private List<AttackScriptableObject> editingAbilities;

    private void OnEnable()
    {
        playerAbilities = GameObject.FindObjectOfType<PlayerAbilities>();
        editingAbilities = new List<AttackScriptableObject>(playerAbilities.EquippedAbilities);

        foreach (AttackSlot slot in Enum.GetValues(typeof(AttackSlot)))
        {
            if (slot == AttackSlot.None) continue;
            if (editingAbilities[(int)slot] != null)
            {
                buttons[(int)slot].GetComponent<Image>().sprite = editingAbilities[(int)slot].Sprite;
            }
            else
            {
                buttons[(int)slot].GetComponent<Image>().sprite = null;
            }
            buttons[(int)slot].GetComponent<Button>().onClick.RemoveAllListeners();
            buttons[(int)slot].GetComponent<Button>().onClick.RemoveAllListeners();
            buttons[(int)slot].GetComponent<Button>().onClick.AddListener(() =>
            {
                AbilityLibraryUI ui = (AbilityLibraryUI) uiManager.OpenUIAndGet(abilityLibraryUI);
                ui.SetReturnAction((newAbility) => OnNewAbilitySelected(newAbility, slot));
            });
        }
    }

    private void CommitChanges()
    {
        List<AttackScriptableObject> originalAbilities = playerAbilities.EquippedAbilities;
        foreach (AttackSlot slot in Enum.GetValues(typeof(AttackSlot)))
        {
            if (slot == AttackSlot.None) continue;
            if (originalAbilities[(int)slot] == editingAbilities[(int)slot]) continue;
            playerAbilities.EquipAbilityInSlot(editingAbilities[(int)slot], slot);
            //hotbarIcons[(int)slot].GetComponent<Image>().sprite = newAbility.Sprite;
        }
        equippedAbilityChangeEvent.Raise();
    }

    public void OnNewAbilitySelected(AttackScriptableObject newAbility, AttackSlot slot)
    {
        if (newAbility == null) return; // case: nothing selected
        if (newAbility == playerAbilities.EquippedAbilities[(int)slot]) return; // case: same ability selected, no change

        editingAbilities[(int)slot] = newAbility;
        buttons[(int)slot].GetComponent<Image>().sprite = newAbility.Sprite;
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