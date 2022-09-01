using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;




public class Launcher : MonoBehaviourPunCallbacks
{

    #region Private Serializable Fields
    [SerializeField] private byte maxPlayersPerRoom = 4;
    #endregion

    #region Private Fields
    static bool isConnecting;
    #endregion

    #region Monobehaviour Call backs

    private void Awake()
    {
        SyncSceneAutomatically(true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Sync Photon Network Scene Automatically
    /// </summary>
    /// <param name="Value"></param>
    public static void SyncSceneAutomatically(bool value)
    {
        PhotonNetwork.AutomaticallySyncScene = value;
    }

    public static void Connect()
    {
        bool isPhotonNetworkConnected = PhotonNetwork.IsConnected;

        

        if (isPhotonNetworkConnected)
        {
            PhotonNetwork.JoinRandomRoom();
            
        }
        else
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            Debug.Log($"Is Connecting {isConnecting}");
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = MultiPlayerProperties.GameVersion;
        }
    }

    #endregion

    #region MonoBehaviourPunCallbacks Callbacks


    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");

        if (isConnecting)
        {
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }


    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        isConnecting = false;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        bool createRoom = PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom }); ;
        if (createRoom)
        {
            Debug.Log($"A Room created having capacity of {maxPlayersPerRoom} players.");
        }
        else
        {
            Debug.LogWarning("Failed to create/join the room.");
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        // #Critical: We only load if we are the first player, else we rely on `PhotonNetwork.AutomaticallySyncScene` to sync our instance scene.

        byte playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        switch (playerCount)
        {
            case 1:
                Debug.Log("We load the 'Room for 1' ");
                // #Critical
                // Load the Room Level.
                PhotonNetwork.LoadLevel("Room for 1");
                break;

            case 2:
                Debug.Log("We load the 'Room for 2' ");
                // #Critical
                // Load the Room Level.
                PhotonNetwork.LoadLevel("Room for 2");
                break;
        }

    }

    #endregion

}


