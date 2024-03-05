using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using WebSocketSharp;
using ExitGames.Client.Photon;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviourPunCallbacks
{
    private int minute;
    private int second;
    [SerializeField] float timer;
    [SerializeField] int playerCount = 3;
    [SerializeField] Text timerText;

    private void Start()
    {
       // CheckPlayer();
    }

    private void Awake()
    {
        CreatePlayer();
        CheckNickName();

        StartCoroutine(StartTimer());
    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate
            (
                "Player",
                RandomPosition(5),
                Quaternion.identity
            );
    }

    public Vector3 RandomPosition(float distance)
    {
        Vector3 direction = Random.insideUnitSphere;

        direction.Normalize();

        direction *= distance;
        
        direction.y = 1;

        return direction;
    }


    // ������ ������ �� ������ �Ѱ��ִ� �Լ�
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
    }

    public void CheckNickName()
    {
        string nickName = PlayerPrefs.GetString("Nick Name");

        PhotonNetwork.NickName = nickName;

        Debug.Log(nickName);
    }
    /*
    //�÷��̾ ��� �����Ͽ����� Ÿ�̸Ӹ� ������
    public void CheckPlayer()
    {
        Debug.Log(PhotonNetwork.PlayerList.Length);

        if (PhotonNetwork.PlayerList.Length >= playerCount)
        {
            photonView.RPC("RemoteTimer", RpcTarget.All);
        }
    }

    [PunRPC]
    public void RemoteTimer()
    {
        StartCoroutine(StartTimer());
    }
    */
    IEnumerator StartTimer()
    {
        while (true)
        {
            timer -= Time.deltaTime;
            minute = (int)timer / 60;
            second = (int)timer % 60;

            timerText.text = minute.ToString("00") + " : " + second.ToString("00");
            yield return null;

            if(timer <= 0)
            {
                yield break;
            }
        }
    }

}
