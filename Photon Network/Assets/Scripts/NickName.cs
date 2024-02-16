using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class NickName : MonoBehaviourPun
{
    [SerializeField] TextMeshProUGUI nickName;

    void Start()
    {
        nickName.text = photonView.Owner.NickName;
    }

    void Update()
    {
        
    }
}
