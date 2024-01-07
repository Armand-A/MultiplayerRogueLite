using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class AbIndEnemyCounter : MonoBehaviour
{
    [SerializeField] private UnityEvent<int> _enemyCountOnChange;

    private int _enemyCount;

    public int EnemyCount { get { return _enemyCount; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemyCountOnChange.Invoke(++_enemyCount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemyCountOnChange.Invoke(--_enemyCount);
        }
    }
}
