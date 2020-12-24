﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class MainMenuSearch : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject findOpponentPanel = null;
    [SerializeField] private GameObject waitingStatusPanel = null;
    [SerializeField] private TextMeshProUGUI waitingStatusText = null;


    private bool isConnecting = false;

    private const string GameVersion = "0.1";
    private const int MaxPlayerPerRoom = 2;



    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true; 
        
        
    }
    public void FindOpponent()
    {
        isConnecting = true;
        findOpponentPanel.SetActive(false);
        waitingStatusPanel.SetActive(true);

        waitingStatusText.text = "Searching...";

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();

        }
        else
        {
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
        


    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");

        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }

    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        waitingStatusPanel.SetActive(false);
        findOpponentPanel.SetActive(true);

        Debug.Log($"Disconnected due to: {cause}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No cliens are waiting for an oppenent, creating a new room ");

        PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = MaxPlayerPerRoom});


    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client successfully Joined a room");

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if(playerCount != MaxPlayerPerRoom)
        {
            waitingStatusText.text = "Waiting for an Opponent";
            Debug.Log("Client is waiting for an opponent");



        }
        else
        {
            waitingStatusText.text = "Opponent Found";
            Debug.Log("Matching is ready to begin");

        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayerPerRoom)
        {
            waitingStatusText.text = "Opponent Found";
            PhotonNetwork.CurrentRoom.IsOpen = false;
            Debug.Log("Match is ready to begin");

            PhotonNetwork.LoadLevel("SampleScene");
        }
    }
}