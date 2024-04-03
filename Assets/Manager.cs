using UnityEngine;

public class Manager : MonoBehaviour
{
    private bool mouseDown = false;
    private bool onSliceRight = false;
    private bool onSliceStarted = false;
    private bool movable = true;
    void OnEnable()
    {
        EventManager.AddListener("OnMouseDown", OnMouseDown);
        EventManager.AddListener("OnMouseUp", OnMouseUp);
        EventManager.AddListener("OnSliceRight", OnSliceRight);
        EventManager.AddListener("OnSliceLeft", OnSliceLeft);
        EventManager.AddListener("OnSliceCenter", OnSliceCenter);
    }
    void OnDestroy()
    {
        EventManager.RemoveListener("OnMouseDown", OnMouseDown);
        EventManager.RemoveListener("OnMouseUp", OnMouseUp);
        EventManager.RemoveListener("OnSliceRight", OnSliceRight);
        EventManager.RemoveListener("OnSliceLeft", OnSliceLeft);
        EventManager.RemoveListener("OnSliceCenter", OnSliceCenter);
    }
    void Update()
    {
        if(!movable) return;
        if(!mouseDown) return;
        if(!onSliceStarted) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {            
            if (onSliceRight == true){
                Debug.Log("Hit to the right");
            }
            else if (onSliceRight == false){
                Debug.Log("Hit to the left");
            }
            movable = false;
        }
    }
    void OnSliceLeft()
    {
        onSliceRight = false;
        onSliceStarted = true;
    }
    void OnSliceRight()
    {
        onSliceRight = true;
        onSliceStarted = true;
    }
    void OnSliceCenter()
    {
        onSliceStarted = false;
    }
    void OnMouseDown()
    {
        mouseDown = true;
    }
    void OnMouseUp()
    {
        mouseDown = false;
        onSliceStarted = false;
        movable = true;
    }
}
