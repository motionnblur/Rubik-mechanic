using System;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    private Vector3 previousMousePosition;

    void Update()
    {
        Vector3 currentMousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = currentMousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            float directionX = currentMousePosition.x - previousMousePosition.x;
            float directionY = currentMousePosition.y - previousMousePosition.y;

            if(Mathf.Abs(directionX) > Mathf.Abs(directionY))
            {
                if(directionX > 0f)
                {
                    EventManager.TriggerEvent("OnSliceLeft");
                }
                else
                {
                    EventManager.TriggerEvent("OnSliceRight");
                }
            }
            else
            {
                if(directionY > 0f)
                {
                    EventManager.TriggerEvent("OnSliceUp");
                }
                else if(directionY < 0f)
                {
                    EventManager.TriggerEvent("OnSliceDown");
                }

            previousMousePosition = currentMousePosition;
            }
        }
    }
}
