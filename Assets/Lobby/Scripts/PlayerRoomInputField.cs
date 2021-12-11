using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRoomInputField : MonoBehaviour {

    #region Private Constants

    #endregion


    #region MonoBehaviour Callbacks

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>

    // Start is called before the first frame update
    public void Start() {
        
        /*
        string defaultName = string.Empty;
        InputField inputField = GetComponent<InputField>();

        if (inputField != null) {
            if (PlayerPrefs.HasKey(PrefKeys.PLAYER_ROOM_NAME_PREF_KEY)) {
                defaultName = PlayerPrefs.GetString(PrefKeys.PLAYER_ROOM_NAME_PREF_KEY);
                inputField.text = defaultName;
            }
        }
        */

    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sets the room's name of the player, and save it in the PlayerPrefs for future sessions.
    /// </summary>
    /// <param name="value">The name of the room</param>

    public void SetPlayerRoomName(string value) {

        if (string.IsNullOrEmpty(value)) {
            Debug.Log("Room's name is null or empty");
            return;
        }

        PlayerPrefs.SetString(PrefKeys.PLAYER_ROOM_NAME_PREF_KEY, value);
    }



    #endregion

}
