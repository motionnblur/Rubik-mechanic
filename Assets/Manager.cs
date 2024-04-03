using UnityEngine;

public class Manager : MonoBehaviour
{
    private bool mouseDown = false;
    void OnEnable()
    {
        EventManager.AddListener("OnMouseDown", OnMouseDown);
        EventManager.AddListener("OnMouseUp", OnMouseUp);
    }
    void OnDestroy()
    {
        EventManager.RemoveListener("OnMouseDown", OnMouseDown);
        EventManager.RemoveListener("OnMouseUp", OnMouseUp);
    }
    void Update()
    {
        if(!mouseDown) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            
            Vector3 direction = hit.point - Camera.main.transform.position;
            float xDir = Vector3.Dot(direction, Camera.main.transform.right);
            Debug.Log(xDir);
            if (xDir > 0){
                Debug.Log("Hit to the right");
            }
            else if (xDir < 0){
                Debug.Log("Hit to the left");
            }
        }
    }
    void OnMouseDown() => mouseDown = true;
    void OnMouseUp() => mouseDown = false;
}
