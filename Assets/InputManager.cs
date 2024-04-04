using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            EventManager.TriggerEvent("OnMouseDown");
        }
        else if(Input.GetMouseButtonUp(0))
        {
            EventManager.TriggerEvent("OnMouseUp");
        }
    }
}
