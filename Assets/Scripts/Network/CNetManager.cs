using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CNetManager : NetworkManager
{
   // public static event Action<NetworkConnection>  OnServerReadied;//When all players joined the server (use in Lobby)
   // public List<NetworkIdentity> spawnablePrefabs = new List<NetworkIdentity>();

   
   public static void OnBulletInstantiated(GameObject bulletPrefab){
      NetworkServer.Spawn(bulletPrefab);
   }
   // public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("Spawnable Prefabs").ToList();
   // public override void OnStartClient(){
   //    ClientScene.RegisterPrefab(PlayerPrefab);
   // }
   public void OnHostButton(){
      StartHost();
   }

   public void OnClientButton(){
      StartClient();
   }
}
