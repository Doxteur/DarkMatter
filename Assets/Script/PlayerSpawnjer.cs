using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnjer : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab = null;

    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero,Quaternion.identity);
    }
}
