using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Player name input field. Let the user input his name, will appear above the player in the game.
/// </summary>
[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour {

    #region Private Constants

    

    #endregion

    #region MonoBehaviour Callbacks

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>

    // Start is called before the first frame update
    public void Start() {

        string defaultName = string.Empty;
        InputField inputField = GetComponent<InputField>();


        if (inputField != null) {
            if (PlayerPrefs.HasKey(PrefKeys.PLAYER_NAME_PREF_KEY)) {
                defaultName = PlayerPrefs.GetString(PrefKeys.PLAYER_NAME_PREF_KEY);
                inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    #endregion

    #region Public Methods


    /// <summary>
    /// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
    /// </summary>
    /// <param name="value">The room's name of the Player</param>
    public void SetPlayerName(string value) {

        if (string.IsNullOrEmpty(value)) {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;
        

        PlayerPrefs.SetString(PrefKeys.PLAYER_NAME_PREF_KEY, value);
    }


    #endregion
}
