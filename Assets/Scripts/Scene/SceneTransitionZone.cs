using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionZone : MonoBehaviour
{
    [SerializeField] string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        PlayerData playerData = other.gameObject.GetComponent<PlayerData>();
        if (playerData == null) playerData = other.gameObject.GetComponentInParent<PlayerData>();
        if (playerData == null) playerData = other.gameObject.GetComponentInChildren<PlayerData>();
        if (playerData == null) return;

        SceneManager.LoadScene(sceneName);
    }
}
