using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class PlayerInfo
{

    #region Private Serialize Fields
    #endregion

    #region Public Methods
    public static void GetPlayerList()
    {
        Player[] playerList = PhotonNetwork.PlayerListOthers;
        if (playerList.Length > 0)
        {
            foreach (Player player in playerList)
            {
                Debug.Log($"Player - {player}");
            }
        }
        else
        {
            Debug.LogWarning("No players are found.");
        }
    }
    #endregion
}
