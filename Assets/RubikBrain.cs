using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubikBrain : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.AddListener("OnRotateRight", OnRotateRight);
        EventManager.AddListener("OnRotateLeft", OnRotateLeft);
        EventManager.AddListener("OnRotateUp", OnRotateUp);
        EventManager.AddListener("OnRotateDown", OnRotateDown);
    }
    void OnDestroy()
    {
        EventManager.RemoveListener("OnRotateRight", OnRotateRight);
        EventManager.RemoveListener("OnRotateLeft", OnRotateLeft);
        EventManager.RemoveListener("OnRotateUp", OnRotateUp);
        EventManager.RemoveListener("OnRotateDown", OnRotateDown);
    }

    private void OnRotateRight(GameObject pivot)
    {
        StartCoroutine(RotateObjectLeftRight(pivot, 90f));
    }
    private void OnRotateLeft(GameObject pivot)
    {
        StartCoroutine(RotateObjectLeftRight(pivot, -90f));
    }
    private void OnRotateUp(GameObject pivot)
    {
        StartCoroutine(RotateObjectUpDown(pivot, 90f));
    }
    private void OnRotateDown(GameObject pivot)
    {
        StartCoroutine(RotateObjectUpDown(pivot, -90f));
    }

    private IEnumerator RotateObjectLeftRight(GameObject pivotObj, float rotateVal)
    {
        Global.isRotationAnimPlayingNow = true;

        Quaternion targetRot = pivotObj.transform.rotation * Quaternion.Euler(0, rotateVal, 0);

        while (Quaternion.Angle(pivotObj.transform.rotation, targetRot) > 0.01f)
        {
            pivotObj.transform.rotation = Quaternion.Lerp(pivotObj.transform.rotation, targetRot, Time.deltaTime * 3.5f); // rotate 2 degree per second
            yield return null; // wait for the next frame
        }

        pivotObj.transform.rotation = targetRot;

        Global.isRotationAnimPlayingNow = false;
        EventManager.TriggerEvent("OnPivotReset", pivotObj);
    }

    private IEnumerator RotateObjectUpDown(GameObject pivotObj, float rotateVal)
    {
        Global.isRotationAnimPlayingNow = true;

        Quaternion targetRot = pivotObj.transform.rotation * Quaternion.Euler(rotateVal, 0, 0);

        while (Quaternion.Angle(pivotObj.transform.rotation, targetRot) > 0.01f)
        {
            pivotObj.transform.rotation = Quaternion.Lerp(pivotObj.transform.rotation, targetRot, Time.deltaTime * 3.5f); // rotate 2 degree per second
            yield return null; // wait for the next frame
        }

        pivotObj.transform.rotation = targetRot;

        Global.isRotationAnimPlayingNow = false;
        EventManager.TriggerEvent("OnPivotReset", pivotObj);
    }
}
