using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    // Ability settings
    [SerializeField] private AttackIndicator attackIndicator;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Ability nextUpgrade;
    [SerializeField] private float nextUpgradePrice;
    [SerializeField] private bool cannotCastOnEnemy;
    public AttackIndicator AttackIndicator { get { return attackIndicator; } }
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



    // Behaviours
    [SerializeField] private bool isInstantiateAtDestination;
    [SerializeField] private GameEvent playerHitsEnemyEvent;
    [SerializeField] private GameEvent enemyHitsPlayerEvent;
    public bool IsInstantiateAtDestination { get { return isInstantiateAtDestination; } }
    public void RaisePlayerHitsEnemyEvent()
    {
        if (playerHitsEnemyEvent != null)
        {
            playerHitsEnemyEvent.Raise();
        }
    }
    public void RaiseEnemyHitsPlayerEvent()
    {
        if (enemyHitsPlayerEvent != null)
        {
            enemyHitsPlayerEvent.Raise();
        }
    }

    Vector3 _srcPos = Vector3.zero;
    Vector3 _dstPos = Vector3.zero;
    bool _isFromPlayer = false;
    List<EntityData> _damagedEntities = new List<EntityData>();
    public Vector3 SrcPos { get { return _srcPos; } }
    public Vector3 DstPos { get { return _dstPos; } }
    public bool IsFromPlayer { get { return _isFromPlayer; } }
    public List<EntityData> DamagedEntities { get { return _damagedEntities; } }



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

    
}
