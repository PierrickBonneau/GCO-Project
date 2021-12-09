using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks {

    #region Private Serializable Fields

    /// <summary>
    /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
    /// </summary>
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players")]
    [SerializeField]
    private byte maxPlayersPerRoom = 2;

    #endregion


    #region Private Fields
    
    private readonly string gameVersion = "1";

    #endregion

    #region Public Fields

    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject controlPanel;

    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject progressLabel;

    #endregion

    #region Monobehaviour Callbacks

    public void Awake() {
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    // Start is called before the first frame update
    public void Start() {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    #endregion

    #region Public Methods

    public void Connect() {

        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        if (PhotonNetwork.IsConnected) {

            string roomName = GetPlayerRoomNamePref();
            PhotonNetwork.JoinRoom(roomName);

        } else {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }

    }



  

    #endregion

    #region Private Methods

    // TODO à déplacer dans une classe utilitaire
    private string GetPlayerRoomNamePref() {

        if (PlayerPrefs.HasKey(PrefKeys.PLAYER_ROOM_NAME_PREF_KEY)) {
            return PlayerPrefs.GetString(PrefKeys.PLAYER_ROOM_NAME_PREF_KEY);
        } else {
            return string.Empty;
        }
    }


    #endregion

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster() {
        Debug.Log("OnConnectedToMaster() was called by PUN");

        string roomName = GetPlayerRoomNamePref();
        PhotonNetwork.JoinRoom(roomName);
    
    }

    public override void OnJoinRoomFailed(short returnCode, string message) {
        Debug.Log("OnJoinRoomFailed() was called by PUN. No room with this name available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        string roomName = GetPlayerRoomNamePref();


        // #Critical: we failed to join a room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = maxPlayersPerRoom });

    }

    public override void OnDisconnected(DisconnectCause cause) {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);

        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public override void OnJoinedRoom() {

        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");

        if (PhotonNetwork.PlayerListOthers.Length != 0) {

            Debug.Log("Les autres joueurs présents sont :");

            foreach(Player player in PhotonNetwork.PlayerListOthers) {
                Debug.Log(player.NickName +"\n");
            }

        } else {
            Debug.Log("Il n'y a aucun autre joueurs dans cette Room actuellement");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {

        if (PhotonNetwork.PlayerList.Length == maxPlayersPerRoom) {

        }
    }

    #endregion
}
