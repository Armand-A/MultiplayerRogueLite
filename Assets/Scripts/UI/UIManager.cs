using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    Stack<AbilityUI> activeUIStack = new Stack<AbilityUI>();
    PlayerInputHandler playerInputHandler;

    protected void Awake()
    {
        playerInputHandler = FindObjectOfType<PlayerInputHandler>();
    }

    public void OpenUI(AbilityUI uiPrefab)
    {
        OpenUIAndGet(uiPrefab);
    }

    public AbilityUI OpenUIAndGet(AbilityUI uiPrefab)
    {
        if (activeUIStack.Count == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;

            if (playerInputHandler != null) playerInputHandler.DisableInputHandling();
        }

        AbilityUI uiObject = Instantiate(uiPrefab.gameObject).GetComponent<AbilityUI>();
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
