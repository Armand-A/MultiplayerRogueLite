using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    protected UIManager uiManager = null;
    public void SetUIManager(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }
}
