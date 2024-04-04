using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private bool mouseDown = false;
    private bool onSliceRight = false;
    private bool onSliceStarted = false;
    private bool movable = true;
    private GameObject [] cubes = new GameObject[54];
    [SerializeField] private GameObject rubik;
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
    private void Start()
    {
        cubes = GameObject.FindGameObjectsWithTag("cube");
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
                
                GameObject[] childCubes = FindGameObjectsWithYPosition(hit.point.y);
                Vector3 center = GetCenter(childCubes);

                GameObject pivotObj = new GameObject("Pivot");
                pivotObj.transform.SetParent(rubik.transform);
                pivotObj.transform.position = center;

                foreach (GameObject obj in childCubes)
                {
                    obj.transform.SetParent(pivotObj.transform);
                }

                EventManager.TriggerEvent("OnRotateRight", hit.point);
            }
            else if (onSliceRight == false){
                Debug.Log("Hit to the left");
                EventManager.TriggerEvent("OnRotateLeft", hit.point);
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
    GameObject[] FindGameObjectsWithYPosition(float yPos)
    {
        GameObject[] returnArr = new GameObject[9];
        int counter = 0;
        foreach (GameObject obj in cubes)
        {
            if (Mathf.Abs(obj.transform.position.y - yPos) < 0.5f) // Check if the y position is close to 0.5
            {
                Debug.Log("Found GameObject: " + obj.name);
                returnArr[counter++] = obj;
            }
        }
        return returnArr;
    }
    Vector3 GetCenter(GameObject[] objects)
    {
        Vector3 center = Vector3.zero;

        foreach (GameObject obj in objects)
        {
            center += obj.transform.position;
        }
        center /= objects.Length;

        return center;
    }
}
