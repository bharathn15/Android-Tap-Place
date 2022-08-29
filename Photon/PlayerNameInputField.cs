using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;


[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{

    #region private Constants

    const string playerNamePrefKey = "ABC";

    #endregion


    #region MonoBehaviour Callbacks

    // Start is called before the first frame update
    void Start()
    {
        string defaultName = string.Empty;
        InputField _inputField = this.GetComponent<InputField>();
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }


        PhotonNetwork.NickName = defaultName;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Public Methods


    /// <summary>
    /// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
    /// </summary>
    /// <param name="value">The name of the Player</param>
    public void SetPlayerName(string value)
    {
        // #Important
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;


        PlayerPrefs.SetString(playerNamePrefKey, value);
    }




    #endregion
}
