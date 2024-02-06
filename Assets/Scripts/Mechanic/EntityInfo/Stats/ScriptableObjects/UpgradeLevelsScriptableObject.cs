using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Levels", menuName = "Player Upgrade Levels")]
public class UpgradeLevelsScriptableObject : ScriptableObject
{
    [SerializeField] List<UpgradeScriptableObject> upgradeScriptableObjects;
    [SerializeField] int _upgradeID;
    [SerializeField] bool _allowDuplicates;

    public List<UpgradeScriptableObject> UpgradeObjects { get { return upgradeScriptableObjects; } }
    public int UpgradeID { get { return _upgradeID; } }
    public bool AllowDuplicates { get { return _allowDuplicates; } }
}