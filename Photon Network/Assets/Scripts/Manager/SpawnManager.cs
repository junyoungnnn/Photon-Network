using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    WaitForSeconds waitForSeconds = new WaitForSeconds(5);

    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(SpawnObject());
        }
    }

    public IEnumerator SpawnObject()
    {
        while(true) 
        {
            // InstantiateRoomObject: 방장이 방을 나가도 파괴가 되지않음
            PhotonNetwork.InstantiateRoomObject("Metalon", RandomPosition(15), Quaternion.identity);

            yield return waitForSeconds;
        }
    }

    public Vector3 RandomPosition(float distance)
    {
        Vector3 direction = Random.insideUnitSphere;

        direction.Normalize();

        direction *= distance;

        direction.y = 0;

        return direction;
    }
}
