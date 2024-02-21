using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityImbueLibraryUI : AbilityLibraryUI
{
    [SerializeField] AbilityLibraryUI abilityImbueOptionsLibraryUI;
    public void OpenImbueOptionsUIAndSetAbility(Ability ability)
    {
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UI Manager not found!");
            return;
        }

        AbilityLibraryUI abilityImbueOptionsLibraryUiInstance = (AbilityLibraryUI) uiManager.OpenUIAndGet(abilityImbueOptionsLibraryUI);
        AbilityOfImbueOptions = ability;
        abilityImbueOptionsLibraryUiInstance.AbilityOfImbueOptions = ability;
        abilityImbueOptionsLibraryUiInstance.UpdateUI();

        abilityImbueOptionsLibraryUiInstance.AddOnAbilityClickedListener((Ability ability) =>
        {
            PlayerAbilities playerAbilities = GameObject.FindObjectOfType<PlayerAbilities>();
            playerAbilities.ImbueAbility(AbilityOfImbueOptions, ability);
            AbilityOfImbueOptions = null;
            uiManager.CloseUI();
            UpdateUI();
        });
    }
}
