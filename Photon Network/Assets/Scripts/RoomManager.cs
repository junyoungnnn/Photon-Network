using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime; // ��� ������ �������� �� �̺�Ʈ�� ȣ���ϴ� ���̺귯��
using UnityEngine.UI;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button roomCreateButton;
    public Transform roomParentTrasform;
    public TMP_InputField roomNameInputField;
    public TMP_InputField roomPersonnelInputField;

    // �� ����� �������� ���� �ڷᱸ��
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

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void InstantiateRoom()
    {
        // �� �ɼ��� �����մϴ�.
        RoomOptions roomOptions = new RoomOptions();

        // �ִ� �������� ���� �����մϴ�.
        roomOptions.MaxPlayers = byte.Parse(roomPersonnelInputField.text);

        // ���� ���� ���θ� �����մϴ�.
        roomOptions.IsOpen = true;

        // �κ񿡼� �� ����� �����ų �� �����մϴ�.
        roomOptions.IsVisible = true;

        // ���� �����ϴ� �Լ�
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
    }

    // �ش� �κ� �� ����� ���� ������ ������ ȣ��(�߰�, ����, ����)�Ǵ� �ݹ� �Լ�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // 1. ���� �����մϴ�.
        RemoveRoom();

        // 2. ���� ������Ʈ �մϴ�.
        UpdateRoom(roomList);

        // 3. ���� �����մϴ�.
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
            // �ش� �̸��� roomDictionary�� key ������ �����Ǿ� �ִٸ�
            if (roomDictionary.ContainsKey(roomList[i].Name))
            {
                // RemoveFromList : (true) �뿡�� �����Ǿ��� ��
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
        // roomDictionary�� ���� ���� Values ���� ����ִٸ� room Info�� �־��ݴϴ�.
        foreach (RoomInfo roomInfo in roomDictionary.Values)
        {
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));

            // room ������Ʈ�� �θ� ������Ʈ�� �����մϴ�.
            room.transform.SetParent(roomParentTrasform);

            // room�� ���� ������ �Է��մϴ�.
            room.GetComponent<Information>().RoomData(roomInfo.Name, roomInfo.PlayerCount, roomInfo.MaxPlayers);


        }

        
    }
}
