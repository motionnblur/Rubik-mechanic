using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubikBrain : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.AddListener("OnRotateRight", OnRotateRight);
        EventManager.AddListener("OnRotateLeft", OnRotateLeft);
    }
    void OnDestroy()
    {
        EventManager.RemoveListener("OnRotateRight", OnRotateRight);
        EventManager.RemoveListener("OnRotateLeft", OnRotateLeft);
    }

    private void OnRotateRight(Vector3 pos)
    {
        if(pos.y > 0.5f && pos.y < 1.5f)
        {
            
        }
    }
    private void OnRotateLeft(Vector3 pos)
    {
        Debug.Log(pos);

        if(pos.y > 0.5f && pos.y < 1.5f)
        {
            
        }
    }
}
