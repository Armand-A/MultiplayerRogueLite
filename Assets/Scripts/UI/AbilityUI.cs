using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    protected UIManager uiManager = null;

    protected void Start()
    {
        if (uiManager == null) throw new System.Exception("UI Manager not found. To open ability UI's, you need to add a UI Manager component in the scene and open the UI through the UI Manager.");
    }

    public void SetUIManager(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public void OpenUI(AbilityUI uiPrefab)
    {
        uiManager.OpenUIAndGet(uiPrefab);
    }
}
