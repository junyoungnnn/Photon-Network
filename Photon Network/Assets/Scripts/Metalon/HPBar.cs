using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public GameObject playerCamera;
    
    public void Start()
    {
        playerCamera = GameObject.Find("MainCamera");
    }

    public void Update()
    {
        transform.forward = playerCamera.transform.forward;
    }
    

}
