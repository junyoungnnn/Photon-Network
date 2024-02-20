using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using PlayFab.ClientModels;

public class NickName : MonoBehaviourPun
{
    //GetAccountInfoResult getAccountInfoResult;
    [SerializeField] TextMeshProUGUI nickName;
    [SerializeField] Camera playerCamera;

    void Start()
    {
        // Debug.Log("Playfab ID" + getAccountInfoResult.AccountInfo.TitleInfo.DisplayName);
        // Debug.Log("PhotonNetwork ID" + PhotonNetwork.NickName);
        // nickName.text = PhotonNetwork.NickName;
        nickName.text = photonView.Owner.NickName;
    }

    void Update()
    {
        // 카메라 방향으로 닉네임이 바라봄
        transform.forward = playerCamera.transform.forward;
    }
}
