using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PhotonPlayer : MonoBehaviourPun, IPunObservable
{
    [SerializeField] int score;
    [SerializeField] float speed;
    [SerializeField] float mouseX;
    [SerializeField] float rotateSpeed;

    [SerializeField] Vector3 direction;
    [SerializeField] Camera temporaryCamera;

    void Start()
    {
        // ���� �÷��̾ �� �ڽ��̶��
        if(photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            temporaryCamera.enabled = false;
            GetComponent<AudioListener>().enabled = false;
        }
    }

    void Update()
    {
        /*
        if (PhotonNetwork.IsMasterClient == true)
        {
            Debug.Log("Master Client");
        }
        */

        if (photonView.IsMine == false) return;

        Movement();

        Rotation();
    }

    public void Movement()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        direction.Normalize();

        // TransformDirection : �ڱⰡ �ٶ󺸰� �ִ� �������� �̵��ϴ� �Լ��Դϴ�.
        transform.position += transform.TransformDirection(direction) * speed * Time.deltaTime;
    }

    public void Rotation()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * rotateSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // ���� ������Ʈ��� ���� �κ��� �����մϴ�.
        if(stream.IsWriting)
        {
            // ��Ʈ��ũ�� ���� �����͸� �����ϴ�.
            stream.SendNext(score);
        }
        else // ���� ������Ʈ��� �б� �κ��� �����մϴ�.
        {
            // ��Ʈ��ũ�� ���ؼ� �����͸� �޽��ϴ�.
            score = (int)stream.ReceiveNext();
        }
    }
}
