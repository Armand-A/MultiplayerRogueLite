using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    Stack<ShopUI> activeUIStack = new Stack<ShopUI>();
    PlayerInputHandler playerInputHandler;

    protected void Awake()
    {
        playerInputHandler = FindObjectOfType<PlayerInputHandler>();
    }

    public void OpenUI(ShopUI uiPrefab)
    {
        OpenUIAndGet(uiPrefab);
    }
    
    public ShopUI OpenUIAndGet(ShopUI uiPrefab)
    {
        if (activeUIStack.Count == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;

            if (playerInputHandler != null) playerInputHandler.DisableInputHandling();
        }

        ShopUI uiObject = Instantiate(uiPrefab.gameObject).GetComponent<ShopUI>();
        uiObject.SetUIManager(this);
        activeUIStack.Push(uiObject);
        return uiObject;
    }

    public void CloseUI()
    {
        if (activeUIStack.Count == 0) return;
        
        Destroy(activeUIStack.Pop().gameObject);
        
        if (activeUIStack.Count == 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            if (playerInputHandler != null) playerInputHandler.EnableInputHandling();
        }
    }
}
