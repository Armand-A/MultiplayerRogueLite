using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EntityDataTypes;

[DisallowMultipleComponent]
public abstract class Ability : MonoBehaviour
{
    // Type definitions
    public enum EAcquisitionType
    {
        Base, 
        BaseImbued, 
        DropOnly
    }

    [System.Serializable]
    public struct ImbueOption
    {
        public ElementTypes elementType;
        public Ability resultAbility;
    }

    [System.Serializable]
    public struct CombineOption
    {
        public Ability otherAbility;
        public Ability resultAbility;
    }




    // Inspector fields
    [Header("General")]
    [SerializeField] string abilityName;
    [SerializeField] Sprite sprite;
    [SerializeField] string description;
    [SerializeField] EAcquisitionType acquisitionType;
    [SerializeField] List<ImbueOption> imbueOptions;
    [SerializeField] List<CombineOption> combineOptions;

    [Header("Costs")]
    [SerializeField] private float healthCost;
    [SerializeField] private float manaCost;
    [SerializeField] private float actionCost;

    [Header("Cooldown")]
    [SerializeField] private float cooldownTime;

    [Header("Damage")]
    [SerializeField] private float damage;
    [SerializeField] private bool isDestroyOnDamage;
    [SerializeField] private bool isDealDamageOnInterval;
    [SerializeField] private float dealDamageInterval;

    [Header("Event refs")]
    [SerializeField] private GameEvent playerHitsEnemyEvent;
    [SerializeField] private GameEvent enemyHitsPlayerEvent;

    // Public getters for inspector fields
    public string AbilityName { get { return abilityName; } }
    public Sprite Sprite { get { return sprite; } }
    public string Description { get { return description; } }
    public EAcquisitionType AcquisitionType { get { return acquisitionType; } }
    public List<ImbueOption> ImbueOptions { get { return imbueOptions; } }
    public float HealthCost {get {return healthCost;}}
    public float ManaCost {get {return manaCost;}}
    public float ActionCost {get {return actionCost;}}
    public float Damage { get { return damage; } }
    public float CooldownTime { get {  return cooldownTime; } }
    public bool IsDestroyOnDamage { get { return isDestroyOnDamage; } }
    public bool IsDealDamageOnInterval { get { return isDealDamageOnInterval; } }
    public float DealDamageInterval { get { return dealDamageInterval; } }




    // Protected fields
    protected Vector3 _srcPos = Vector3.zero;
    protected EntityData _sourceEntity;
    protected bool _initialized = false;

    // Public getters for protected fields
    public Vector3 SrcPos { get { return _srcPos; } }
    public EntityData SourceEntity { get { return _sourceEntity; } }






    // derived classes need to override and implement this
    public abstract bool Initialize(Vector3 srcPos, EntityData sourceEntity, Vector3? dstPos, Ray? dir, GameObject targetObject = null);





    // Public methods
    public bool CanDealDamageToEntity(EntityData entity) => 
        (entity is EnemyData && _sourceEntity is PlayerData) ||
        (entity is PlayerData && _sourceEntity is EnemyData);

    public void DealDamageToEntity(EntityData target)
    {
        if (CanDealDamageToEntity(target))
        {
            target.Damage(damage);
            if (target is EnemyData)
            {
                if (playerHitsEnemyEvent != null)
                {
                    playerHitsEnemyEvent.Raise();
                }
            }
            else if (target is PlayerData)
            {
                if (enemyHitsPlayerEvent != null)
                {
                    enemyHitsPlayerEvent.Raise();
                }
            }
        }
    }
}
