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

    private void OnRotateRight(GameObject pivot)
    {
        
    }
    private void OnRotateLeft(GameObject pivot)
    {
        
    }
}
