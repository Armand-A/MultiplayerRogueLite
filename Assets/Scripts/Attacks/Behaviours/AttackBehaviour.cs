using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] private bool isInstantiateAtDestination;

    protected Vector3 _srcPos = Vector3.zero;
    protected Vector3 _dstPos = Vector3.zero;

    protected float _damage = 0f;

    protected bool _isFromPlayer = false;

    public void SetPositions(Vector3 srcPos, Vector3 dstPos)
    {
        _srcPos = srcPos;
        _dstPos = dstPos;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void SetIsFromPlayer(bool isFromPlayer)
    {
        _isFromPlayer = isFromPlayer;
    }

    public bool GetIsInstantiateAtDestination()
    {
        return isInstantiateAtDestination; 
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        EntityData entity = other.gameObject.GetComponent<EntityData>();
        if (entity == null) entity = other.gameObject.GetComponentInParent<EntityData>();
        if (entity == null) entity = other.gameObject.GetComponentInChildren<EntityData>();
        
        if (entity != null)
        {
            if
            (
                (entity is EnemyData && _isFromPlayer) ||
                (entity is PlayerData && !_isFromPlayer)
            )
            {
                entity.UpdateHealth(-_damage);
            }
        }
    }
}
