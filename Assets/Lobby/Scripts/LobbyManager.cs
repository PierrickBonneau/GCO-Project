using Photon.Pun;
using Photon.Realtime;
using System;
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
    private byte maxPlayersPerRoom = 4;

    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject mainMenuPanel;

    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject progressLabel;

    [Tooltip("The pre-game UI Panel that gives all the infos about the current room")]
    [SerializeField]
    private GameObject preGamePanel;

    [SerializeField]
    private GameObject playerPanel1;

    [SerializeField]
    private GameObject playerPanel2;

    [SerializeField]
    private GameObject playerPanel3;

    [SerializeField]
    private GameObject playerPanel4;


    #endregion


    #region Private Fields

    private readonly string gameVersion = "1";

    #endregion

    #region Public Fields


    #endregion

    #region Monobehaviour Callbacks

    public void Awake() {
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    // Start is called before the first frame update
    public void Start() {
        progressLabel.SetActive(false);
        mainMenuPanel.SetActive(true);
        preGamePanel.SetActive(false);
    }

    #endregion

    #region Public Methods

    public void Connect() {

        mainMenuPanel.SetActive(false);
        progressLabel.SetActive(true);
        preGamePanel.SetActive(false);

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

    // get the room selected by the player in the start menu
    private string GetPlayerRoomNamePref() {

        if (PlayerPrefs.HasKey(PrefKeys.PLAYER_ROOM_NAME_PREF_KEY)) {
            return PlayerPrefs.GetString(PrefKeys.PLAYER_ROOM_NAME_PREF_KEY);
        } else {
            return string.Empty;
        }
    }

    private bool IsRoomFull() {
        return PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersPerRoom;
    }

    private void StartGame() {

            Debug.Log("Tous les joueurs sont là, la partie peut commencer !");
            PhotonNetwork.LoadLevel("Room 1");
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
        mainMenuPanel.SetActive(true);
    }

    public override void OnJoinedRoom() {

        preGamePanel.SetActive(true);
        progressLabel.SetActive(false);

        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");

        Player[] otherPlayers = PhotonNetwork.PlayerListOthers;

        playerPanel1.GetComponentInChildren<Text>().text = PhotonNetwork.NickName;

        try {
            if (otherPlayers[0] != null)
                playerPanel2.GetComponentInChildren<Text>().text = otherPlayers[0].NickName;

            if (otherPlayers[1] != null)
                playerPanel3.GetComponentInChildren<Text>().text = otherPlayers[1].NickName;

            if (otherPlayers[2] != null)
                playerPanel4.GetComponentInChildren<Text>().text = otherPlayers[2].NickName;
        } catch(IndexOutOfRangeException e) {

        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {

        int othersPlayersCount = PhotonNetwork.PlayerListOthers.Length;

        if (othersPlayersCount == 0) {
            playerPanel1.GetComponentInChildren<Text>().text = newPlayer.NickName;
        } else if (othersPlayersCount == 1) {
            playerPanel2.GetComponentInChildren<Text>().text = newPlayer.NickName;
        } else if (othersPlayersCount == 2) {
            playerPanel3.GetComponentInChildren<Text>().text = newPlayer.NickName;
        } else if (othersPlayersCount == 3) {
            playerPanel4.GetComponentInChildren<Text>().text = newPlayer.NickName;
        }

    }

    #endregion
}
