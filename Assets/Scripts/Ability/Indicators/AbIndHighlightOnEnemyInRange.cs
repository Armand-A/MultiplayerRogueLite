using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer)), AddComponentMenu("Ability/Indicator/Highlight/Highlight on Enemy in Range")]
public class AbIndHighlightOnEnemyInRange : MonoBehaviour
{
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material normalMaterial;

    Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void OnEnemyCounterChange(int counter)
    {
        if (counter > 0)
        {
            _renderer.material = highlightMaterial;
        }
        else
        {
            _renderer.material = normalMaterial;
        }
    }
}
