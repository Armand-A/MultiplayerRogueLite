using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDespawnOnTrigger : MonoBehaviour
{
    [SerializeField] List<string> collidingLayerNames = new List<string>();

    List<int> collidingLayers = new List<int>();

    private void Awake()
    {
        BuildLayerList();
    }

    private void OnValidate()
    {
        BuildLayerList();
    }

    void BuildLayerList()
    {
        collidingLayers.Clear();
        foreach (string layerName in collidingLayerNames)
        {
            collidingLayers.Add(LayerMask.NameToLayer(layerName));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (int layer in collidingLayers)
        {
            if (other.gameObject.layer == layer)
            {
                Destroy(gameObject); // TODO: object pooling
                return;
            }
        }
    }
}
