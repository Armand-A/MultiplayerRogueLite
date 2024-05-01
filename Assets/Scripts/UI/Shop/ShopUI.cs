using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    protected UIManager uiManager = null;

    private void Start()
    {
        if (uiManager == null) throw new System.Exception("UI Manager not found. To open ability UI's, you need to add a UI Manager component in the scene and open the UI through the UI Manager.");
    }

    public void SetUIManager(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public void OpenUI(ShopUI uiPrefab)
    {
        uiManager.OpenUIAndGet(uiPrefab);
    }

    public void CloseUI()
    {
        uiManager.CloseUI();
    }
}