using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Renderer))]
public class AttackIndicator : MonoBehaviour
{
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material normalMaterial;

    protected Vector3 _srcPos = Vector3.zero;
    protected Vector3 _dstPos = Vector3.zero;

    private int _enemyCountInCollider;
    private bool hasEnemyInRange;

    public bool HasEnemyInRange {  get { return hasEnemyInRange; } }

    public void SetPositions(Vector3 srcPos, Vector3 dstPos)
    {
        _srcPos = srcPos;
        _dstPos = dstPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemyCountInCollider++;
            if (highlightMaterial != null && normalMaterial != null)
            {
                hasEnemyInRange = true;
                GetComponent<Renderer>().material = highlightMaterial;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemyCountInCollider--;
            if (_enemyCountInCollider == 0 && highlightMaterial != null && normalMaterial != null)
            {
                hasEnemyInRange = false;
                GetComponent<Renderer>().material = normalMaterial;
            }
        }
    }
}
