using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Melee = 0, 
    Projectile, 
    AreaOfEffect
}

public class PlayerAttack : MonoBehaviour
{
    private GameObject _camera;
    private GameObject _player;
    private GameObject _indicator;

    public AttackType attackType;

    [Header("Melee Properties")]
    public float meleeRange;

    [Header("Projectile Properties")]
    public GameObject projectileIndicatorPrefab;
    public GameObject projectilePrefab;

    [Header("AreaOfEffect Properties")]
    public float aoeRange = 9f;
    public float aoeDistance = 15f;
    public GameObject aoeIndicatorPrefab;
    public GameObject aoePrefab;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _camera = GameObject.FindWithTag("MainCamera");
    }

    private void Update()
    {
        if (_indicator != null)
        {
            switch (attackType)
            {
                case AttackType.Projectile:
                    _indicator.transform.position = GetProjectilePosition();
                    _indicator.transform.LookAt(GetProjectileLookAt());
                    break;
                   
                case AttackType.AreaOfEffect:
                    _indicator.transform.position = GetAoePosition();
                    break;
            }
        }
    }

    public void OnAttackStarted()
    {
        switch (attackType)
        {
            case AttackType.Melee:
                break;
            
            case AttackType.Projectile:
                _indicator = Instantiate(projectileIndicatorPrefab, _player.transform.position, _player.transform.rotation);
                break;

            case AttackType.AreaOfEffect:
                _indicator = Instantiate(aoeIndicatorPrefab, _player.transform.position, Quaternion.identity);
                _indicator.transform.localScale = new Vector3(aoeRange, _indicator.transform.localScale.y, aoeRange);
                break;
        }
    }

    public void OnAttackCanceled()
    {
        switch (attackType)
        {
            case AttackType.Projectile:
                Destroy(_indicator);
                _indicator = null;

                GameObject projectileObject = Instantiate(projectilePrefab, GetProjectilePosition(), Quaternion.identity);
                projectileObject.transform.LookAt(GetProjectileLookAt());

                break;

            case AttackType.AreaOfEffect:
                Destroy(_indicator);
                _indicator = null;

                GameObject aoeObject = Instantiate(aoePrefab, GetAoePosition(), Quaternion.identity);
                aoeObject.transform.localScale = new Vector3(aoeRange, aoeObject.transform.localScale.y, aoeRange);

                break;
        }
    }
    
    private Vector3 GetProjectilePosition()
    {
        Vector3 position = _player.transform.position;

        return position;
    }


    private Vector3 GetProjectileLookAt()
    {
        Vector3 projectilePosition = GetProjectilePosition();

        Vector3 projectileLookAt = projectilePosition + _camera.transform.forward;
        projectileLookAt.y = projectilePosition.y;

        return projectileLookAt;
    }

    private Vector3 GetAoePosition()
    {
        Vector3 position = _player.transform.position + _camera.transform.forward * aoeDistance;
        position.y = aoePrefab.transform.localScale.y / 2;

        return position;
    }
}
