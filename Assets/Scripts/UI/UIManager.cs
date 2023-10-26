using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    Stack<GameObject> activeUIStack = new Stack<GameObject>();

    public void OpenUI(GameObject uiPrefab)
    {
        OpenUIAndGetObject(uiPrefab);
    }

    public GameObject OpenUIAndGetObject(GameObject uiPrefab)
    {
        GameObject uiObject = Instantiate(uiPrefab);
        activeUIStack.Push(uiObject);
        return uiObject;
    }
}
