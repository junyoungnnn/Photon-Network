using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOUSETYPE
{
    LOCK,
    FREE,
}

public class MouseManager : MonoBehaviour
{
    void Start()
    {
        SetMouse(MOUSETYPE.LOCK);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetMouse(MOUSETYPE.FREE);
        }
    }

    public void LockMouse()
    {
        SetMouse(MOUSETYPE.LOCK);
    }

    public void SetMouse(MOUSETYPE mousetype)
    {
        switch(mousetype)
        {
            case MOUSETYPE.LOCK:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;

            case MOUSETYPE.FREE:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
        }

    }
}
