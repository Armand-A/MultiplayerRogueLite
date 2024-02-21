using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class Ability : MonoBehaviour
{
    // Ability settings
    [SerializeField] private AbIndicator attackIndicator;
    [SerializeField] private Sprite sprite;
    public AbIndicator AttackIndicator { get { return attackIndicator; } }
    public Sprite Sprite { get { return sprite; } }
    
    public enum EAcquisitionType
    {
        Base, 
        BaseImbued, 
        DropOnly
    }

    public enum EElementType
    {
        Fire, Water, Ice, Lightning
    }

    [System.Serializable]
    public struct ImbueOption
    {
        public EElementType elementType;
        public Ability resultAbility;
    }

    [SerializeField] EAcquisitionType acquisitionType;
    public EAcquisitionType AcquisitionType { get { return acquisitionType; } }

    [SerializeField] List<ImbueOption> imbueOptions;
    public List<ImbueOption> ImbueOptions { get { return imbueOptions; } }

    [System.Serializable]
    public struct CombineOption
    {
        public Ability otherAbility;
        public Ability resultAbility;
    }

    [SerializeField] List<CombineOption> combineOptions;



    // Numerical attributes
    [SerializeField] private float healthCost;
    [SerializeField] private float manaCost;
    [SerializeField] private float actionCost;
    [SerializeField] private float damage;
    [SerializeField] private float cooldownTime;
    public float HealthCost {get {return healthCost;}}
    public float ManaCost {get {return manaCost;}}
    public float ActionCost {get {return actionCost;}}
    public float Damage { get { return damage; } }
    public float CooldownTime { get {  return cooldownTime; } }



    // Damage
    [SerializeField] private bool isDestroyOnDamage;
    [SerializeField] private bool isDealDamageOnInterval;
    [SerializeField] private float dealDamageInterval;
    public bool IsDestroyOnDamage { get { return isDestroyOnDamage; } }
    public bool IsDealDamageOnInterval { get { return isDealDamageOnInterval; } }
    public float DealDamageInterval { get { return dealDamageInterval; } }





    // Events
    [SerializeField] private GameEvent playerHitsEnemyEvent;
    [SerializeField] private GameEvent enemyHitsPlayerEvent;






    protected Vector3 _srcPos = Vector3.zero;
    public Vector3 SrcPos { get { return _srcPos; } }
    
    protected EntityData _sourceEntity;
    public EntityData SourceEntity { get { return _sourceEntity; } }

    protected bool _initialized = false;
    public abstract bool Initialize(Vector3 srcPos, EntityData sourceEntity, Vector3? dstPos, Ray? dir, GameObject targetObject = null);

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
