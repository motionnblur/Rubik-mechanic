using UnityEngine;

public class GestureManager : MonoBehaviour
{
    private Vector3 previousMousePosition;

    // Update is called once per frame
    void Update()
    {
        Vector3 currentMousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = currentMousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            float direction = currentMousePosition.x - previousMousePosition.x;

            if (direction > 0)
            {
                EventManager.TriggerEvent("OnSliceRight");
            }
            else if (direction < 0)
            {
                EventManager.TriggerEvent("OnSliceLeft");
            }else{
                EventManager.TriggerEvent("OnSliceCenter");
            }

            previousMousePosition = currentMousePosition;
        }
    }
}
