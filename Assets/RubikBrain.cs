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
        StartCoroutine(RotateObject(pivot, 90f));
    }
    private void OnRotateLeft(GameObject pivot)
    {
        StartCoroutine(RotateObject(pivot, -90f));
    }

    private IEnumerator RotateObject(GameObject pivotObj, float rotateVal)
    {
        Global.isRotationAnimPlayingNow = true;

        Quaternion targetRot = Quaternion.Euler(0, -rotateVal, 0);

        while (Quaternion.Angle(pivotObj.transform.rotation, targetRot) > 0.01f)
        {
            pivotObj.transform.rotation = Quaternion.Lerp(pivotObj.transform.rotation, targetRot, Time.deltaTime * 3.5f); // rotate 2 degree per second
            yield return null; // wait for the next frame
        }

        pivotObj.transform.rotation = targetRot;

        Global.isRotationAnimPlayingNow = false;
    }
}
