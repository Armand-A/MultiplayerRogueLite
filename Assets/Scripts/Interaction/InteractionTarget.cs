using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTarget : MonoBehaviour
{
    [SerializeField] GameObject selectedIndicatorObject;

    private void Awake()
    {
        if (selectedIndicatorObject != null)
        {
            selectedIndicatorObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInteract player = other.gameObject.GetComponentInParent<PlayerInteract>();
        if (player == null) return;

        player.AddTarget(this);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerInteract player = other.gameObject.GetComponentInParent<PlayerInteract>();
        if (player == null) return;

        player.RemoveTarget(this);
    }

    public void SetSelected(bool isSelected)
    {
        if (selectedIndicatorObject != null)
        {
            selectedIndicatorObject.SetActive(isSelected);
        }
    }

    public void OnInteract()
    {
        Debug.Log("interacted with " + gameObject.name);
    }
}
