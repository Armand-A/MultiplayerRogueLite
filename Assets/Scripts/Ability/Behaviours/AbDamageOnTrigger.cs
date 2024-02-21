using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

[AddComponentMenu("Ability/Behaviours/Damage/Damage On Trigger (Detachable)")]
public class AbDamageOnTrigger : MonoBehaviour
{
    [SerializeField] private Ability _ability;

    List<EntityData> entitiesTriggered = new List<EntityData>();
    Dictionary<EntityData, float> entitiesInTriggerDuration = new Dictionary<EntityData, float>();

    EntityData FindEntityInObject(GameObject obj)
    {
        EntityData entity = obj.gameObject.GetComponent<EntityData>();
        if (entity == null) entity = obj.gameObject.GetComponentInParent<EntityData>();
        if (entity == null) entity = obj.gameObject.GetComponentInChildren<EntityData>();
        return entity;
    }

    void OnTriggerEnter(Collider other)
    {
        EntityData entity = FindEntityInObject(other.gameObject);

        if (entity != null)
        {
            if (entitiesTriggered.Contains(entity)) return;
            entitiesTriggered.Add(entity);

            // check for friendly fire
            if (_ability.CanDealDamageToEntity(entity))
            {
                _ability.DealDamageToEntity(entity);

                entitiesInTriggerDuration[entity] = 0f;

                if (_ability.IsDestroyOnDamage)
                {
                    Destroy(_ability.gameObject); // TODO: object pooling
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EntityData entity = FindEntityInObject(other.gameObject);
        if (entity != null)
        {
            entitiesInTriggerDuration.Remove(entity);
            entitiesTriggered.Remove(entity);
        }
    }

    private void Update()
    {
        if (_ability.IsDealDamageOnInterval)
        {
            Dictionary<EntityData, float> newEntityInTriggerDuration = new Dictionary<EntityData, float>();
            foreach (KeyValuePair<EntityData, float> entry in entitiesInTriggerDuration)
            {
                float oldDuration = entitiesInTriggerDuration[entry.Key];
                float newDuration = oldDuration + Time.deltaTime;
                int totalDamageInstancesInflicted = (int)(oldDuration / _ability.DealDamageInterval) + 1;
                int newTotalDamageInstancesInflicted = (int)(newDuration / _ability.DealDamageInterval) + 1;
                int damageInstancesToInflict = newTotalDamageInstancesInflicted - totalDamageInstancesInflicted;
                newEntityInTriggerDuration[entry.Key] = newDuration;

                for (int i = 0; i < damageInstancesToInflict; i++)
                {
                    _ability.DealDamageToEntity(entry.Key);
                }
            }
            entitiesInTriggerDuration = newEntityInTriggerDuration;
        }
    }
}
