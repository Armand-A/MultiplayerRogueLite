using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Ability/Ability")]
public class Ability : MonoBehaviour
{
    // Ability settings
    [SerializeField] private AbIndicator attackIndicator;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Ability nextUpgrade;
    [SerializeField] private float nextUpgradePrice;
    [SerializeField] private bool cannotCastOnEnemy;
    public AbIndicator AttackIndicator { get { return attackIndicator; } }
    public Sprite Sprite { get { return sprite; } }
    public Ability NextUpgrade { get { return nextUpgrade; } }
    public float NextUpgradePrice { get { return nextUpgradePrice; } }
    public bool IsCannotCastOnEnemy {  get { return cannotCastOnEnemy; } }



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



    // Behaviours
    [SerializeField] private bool isInstantiateAtDestination;
    public bool IsInstantiateAtDestination { get { return isInstantiateAtDestination; } }

    [SerializeField] private float destinationSphereCastRadius = 0f;
    public float DestinationSphereCastRadius { get { return destinationSphereCastRadius; } }

    [SerializeField] private bool useCustomIndicatorLocalPosition;
    [SerializeField] private Vector3 customIndicatorLocalPosition;
    [SerializeField] private bool useCustomIndicatorLocalRotation;
    [SerializeField] private Vector3 customIndicatorLocalRotation;
    [SerializeField] private bool useCustomIndicatorLocalScale;
    [SerializeField] private Vector3 customIndicatorLocalScale;
    public bool UseCustomIndicatorLocalPosition { get {  return useCustomIndicatorLocalPosition; } }
    public Vector3 CustomIndicatorLocalPosition { get { return customIndicatorLocalPosition; } }
    public bool UseCustomIndicatorLocalRotation { get {  return useCustomIndicatorLocalRotation; } }
    public Vector3 CustomIndicatorLocalRotation { get { return customIndicatorLocalRotation; } }
    public bool UseCustomIndicatorLocalScale { get {  return useCustomIndicatorLocalScale; } }  
    public Vector3 CustomIndicatorLocalScale { get { return customIndicatorLocalScale; } }


    // Events
    [SerializeField] private GameEvent playerHitsEnemyEvent;
    [SerializeField] private GameEvent enemyHitsPlayerEvent;

    Vector3 _srcPos = Vector3.zero;
    Vector3 _dstPos = Vector3.zero;
    bool _isFromPlayer = false;
    public Vector3 SrcPos { get { return _srcPos; } }
    public Vector3 DstPos { get { return _dstPos; } }
    public bool IsFromPlayer { get { return _isFromPlayer; } }



    bool _initialized = false;
    public bool Initialize(Vector3 srcPos, Vector3 dstPos, bool isFromPlayer)
    {
        if (_initialized) return false;

        _srcPos = srcPos;
        _dstPos = dstPos;
        _isFromPlayer = isFromPlayer;

        _initialized = true;
        return true;
    }

    public bool CanDealDamageToEntity(EntityData entity) => (entity is EnemyData && _isFromPlayer) || (entity is PlayerData && !_isFromPlayer);

    public void DealDamageToEntity(EntityData target)
    {
        target.UpdateHealth(-damage);
        if (CanDealDamageToEntity(target))
        {
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
