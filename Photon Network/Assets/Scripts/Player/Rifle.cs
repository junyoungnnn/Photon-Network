using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    Ray ray;
    RaycastHit raycastHit;
    [SerializeField] int attack = 20;
    [SerializeField] Camera camera;
    [SerializeField] LayerMask layerMask;
    [SerializeField] AudioSource playerAudioSource;
    [SerializeField] AudioClip audioClip;

    void Update()
    {
        Launch();
    }

    public void Launch()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            playerAudioSource.PlayOneShot(audioClip);
            ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if(Physics.Raycast(ray, out raycastHit, Mathf.Infinity, layerMask))
            {
                PhotonView photonView = raycastHit.collider.GetComponent<PhotonView>();

                photonView.GetComponent<Metalon>().Health -= attack;

                if (photonView.GetComponent<Metalon>().Health <= 0)
                {
                    if (photonView.IsMine)
                    {
                        PhotonNetwork.Destroy(photonView.gameObject);
                    }
                }
            }
        }
    }
}
