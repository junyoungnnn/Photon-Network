using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime; // 어느 서버에 접속했을 때 이벤트를 호출하는 라이브러리
using UnityEngine.UI;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button roomCreateButton;
    public Transform roomParentTrasform;
    public TMP_InputField roomNameInputField;
    public TMP_InputField roomPersonnelInputField;
    [SerializeField] TMP_InputField nickNameInputField;
    [SerializeField] GameObject nickNamePanel;

    // 룸 목록을 저장히기 위한 자료구조
    Dictionary<string, RoomInfo> roomDictionary = new Dictionary<string, RoomInfo>();

    void Update()
    {
        if(roomNameInputField.text.Length > 0 && roomPersonnelInputField.text.Length > 0)
        {
            roomCreateButton.interactable = true;
        }
        else
        {
            roomCreateButton.interactable= false;
        }
    }

    public void CreateNickName()
    {
        PlayerPrefs.SetString("Nick Name", nickNameInputField.text);
        Debug.Log(nickNameInputField.text);
        PhotonNetwork.NickName = nickNameInputField.text;

        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    nickNameInputField.ActivateInputField();
        //    return;
        //}
        
        Debug.Log("닉네임 설정 완료");
        nickNamePanel.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void InstantiateRoom()
    {
        // 룸 옵션을 설정합니다.
        RoomOptions roomOptions = new RoomOptions();

        // 최대 접속자의 수를 설정합니다.
        roomOptions.MaxPlayers = byte.Parse(roomPersonnelInputField.text);

        // 룸의 오픈 여부를 설정합니다.
        roomOptions.IsOpen = true;

        // 로비에서 륨 목록을 노출시킬 지 설정합니다.
        roomOptions.IsVisible = true;

        // 룸을 생성하는 함수
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
    }

    // 해당 로비에 방 목록의 변경 사항이 있으면 호출(추가, 삭제, 참가)되는 콜백 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // 1. 룸을 삭제합니다.
        RemoveRoom();

        // 2. 룸을 업데이트 합니다.
        UpdateRoom(roomList);

        // 3. 룸을 생성합니다.
        CreateRoomObject();
    }

    public void RemoveRoom()
    {
        foreach(Transform roomTransform in roomParentTrasform)
        {
            Destroy(roomTransform.gameObject);
        }
    }

    public void UpdateRoom(List<RoomInfo> roomList)
    {
        for(int i = 0; i< roomList.Count; i++)
        {
            // 해당 이름이 roomDictionary의 key 값으로 설정되어 있다면
            if (roomDictionary.ContainsKey(roomList[i].Name))
            {
                // RemoveFromList : (true) 룸에서 삭제되었을 때
                if (roomList[i].RemovedFromList)
                {
                    roomDictionary.Remove(roomList[i].Name);
                }
                else
                {
                    roomDictionary[roomList[i].Name] = roomList[i];
                }
            }
            else
            {
                roomDictionary[roomList[i].Name] = roomList[i];
            }
        }
    }

    public void CreateRoomObject()
    {
        // roomDictionary에 여러 개의 Values 값이 들어있다면 room Info에 넣어줍니다.
        foreach (RoomInfo roomInfo in roomDictionary.Values)
        {
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));

            // room 오브젝트의 부모 오브젝트를 성정합니다.
            room.transform.SetParent(roomParentTrasform);

            // room에 대한 정보를 입력합니다.
            room.GetComponent<Information>().RoomData(roomInfo.Name, roomInfo.PlayerCount, roomInfo.MaxPlayers);
        }
    }

    public void ChangeNickName()
    {
        PlayerPrefs.SetString("Nick Name", "");
    }
}
