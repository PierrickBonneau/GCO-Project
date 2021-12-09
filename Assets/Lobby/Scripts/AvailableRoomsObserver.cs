using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailableRoomsObserver : MonoBehaviourPunCallbacks {

    // TODO Fix




    #region Private Fields 

    private Dropdown _dropdown;

    #endregion


    #region Private Methods 



    #endregion

    #region Public Methods 

    public void Awake() {

        _dropdown = GetComponent<Dropdown>(); // cached

    }


    #endregion

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        Debug.Log("OnRoomListUpdate was called");


       foreach(RoomInfo roomInfo in roomList) {

            _dropdown.options.Add(new Dropdown.OptionData(roomInfo.Name));

        }

    }


    #endregion

}
