using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability))]
public class AbilityDamageOnTrigger : MonoBehaviour
{
    [SerializeField] private bool isDestroyOnDamage;

    Ability _ability;

    void Start()
    {
        _ability = GetComponent<Ability>();
    }

    void OnTriggerEnter(Collider other)
    {
        EntityData entity = other.gameObject.GetComponent<EntityData>();
        if (entity == null) entity = other.gameObject.GetComponentInParent<EntityData>();
        if (entity == null) entity = other.gameObject.GetComponentInChildren<EntityData>();

        if (entity != null)
        {
            if (_ability.DamagedEntities.Contains(entity))
            {
                return;
            }

            bool isFromPlayerToEnemy = entity is EnemyData && _ability.IsFromPlayer;
            bool isFromEnemyToPlayer = entity is PlayerData && !_ability.IsFromPlayer;
            if (isFromPlayerToEnemy || isFromEnemyToPlayer)
            {
                _ability.DamagedEntities.Add(entity);
                entity.UpdateHealth(-_ability.Damage);

                if (isFromPlayerToEnemy)
                {
                    _ability.RaisePlayerHitsEnemyEvent();
                } else if (isFromEnemyToPlayer)
                {
                    _ability.RaiseEnemyHitsPlayerEvent();
                }

                if (isDestroyOnDamage)
                {
                    Destroy(gameObject); // TODO: object pooling
                }
            }
        }
    }
}
