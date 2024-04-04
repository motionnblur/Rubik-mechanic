using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private bool mouseDown = false;
    private bool onSliceUpDown = false;
    private bool onSliceLeftRight = false;
    private bool onSliceRight = false;
    private bool onSliceUp = false;
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
        EventManager.AddListener("OnSliceUp", OnSliceUp);
        EventManager.AddListener("OnSliceDown", OnSliceDown);
        EventManager.AddListener("OnSliceCenter", OnSliceCenter);
    }
    void OnDestroy()
    {
        EventManager.RemoveListener("OnMouseDown", OnMouseDown);
        EventManager.RemoveListener("OnMouseUp", OnMouseUp);
        EventManager.RemoveListener("OnSliceRight", OnSliceRight);
        EventManager.RemoveListener("OnSliceLeft", OnSliceLeft);
        EventManager.RemoveListener("OnSliceUp", OnSliceUp);
        EventManager.RemoveListener("OnSliceDown", OnSliceDown);
        EventManager.RemoveListener("OnSliceCenter", OnSliceCenter);
    }
    private void Start()
    {
        cubes = GameObject.FindGameObjectsWithTag("cube");
    }
    void Update()
    {
        if(Global.isRotationAnimPlayingNow) return;
        if(!movable) return;
        if(!mouseDown) return;
        if(!onSliceStarted) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {            
            if(onSliceLeftRight == true)
            {
                if (onSliceRight == true)
                {                
                    if(hit.collider.gameObject.transform.parent.name.Equals("Pivot"))
                    {
                        EventManager.TriggerEvent("OnRotateRight", hit.collider.gameObject.transform.parent.gameObject);

                        onSliceRight = false;
                        onSliceLeftRight = false;
                        return;
                    }

                    GameObject[] childCubes = FindGameObjectsWithYPosition(hit.point.y);
                    Vector3 center = GetCenter(childCubes);

                    GameObject pivotObj = new GameObject("Pivot");
                    pivotObj.transform.SetParent(rubik.transform);
                    pivotObj.transform.position = center;

                    foreach (GameObject obj in childCubes)
                    {
                        obj.transform.SetParent(pivotObj.transform);
                    }

                    EventManager.TriggerEvent("OnRotateRight", pivotObj);

                    onSliceRight = false;
                    onSliceLeftRight = false;
                }
                else if (onSliceRight == false)
                {
                    if(hit.collider.gameObject.transform.parent.name.Equals("Pivot"))
                    {
                        EventManager.TriggerEvent("OnRotateLeft", hit.collider.gameObject.transform.parent.gameObject);

                        onSliceRight = false;
                        onSliceLeftRight = false;
                        return;
                    }

                    GameObject[] childCubes = FindGameObjectsWithYPosition(hit.point.y);
                    Vector3 center = GetCenter(childCubes);

                    GameObject pivotObj = new GameObject("Pivot");
                    pivotObj.transform.SetParent(rubik.transform);
                    pivotObj.transform.position = center;

                    foreach (GameObject obj in childCubes)
                    {
                        obj.transform.SetParent(pivotObj.transform);
                    }

                    EventManager.TriggerEvent("OnRotateLeft", pivotObj);

                    onSliceRight = true;
                    onSliceLeftRight = false;
                    }
                    movable = false;
                }
            else if (onSliceUpDown == true)
            {
                if (onSliceUp == true)
                {
                    if(hit.collider.gameObject.transform.parent.name.Equals("Pivot"))
                    {
                        EventManager.TriggerEvent("OnRotateUp", hit.collider.gameObject.transform.parent.gameObject);

                        onSliceUp = false;
                        onSliceUpDown = false;
                        return;
                    }

                    GameObject[] childCubes = FindGameObjectsWithXPosition(hit.point.x);
                    Vector3 center = GetCenter(childCubes);

                    GameObject pivotObj = new GameObject("Pivot");
                    pivotObj.transform.SetParent(rubik.transform);
                    pivotObj.transform.position = center;

                    foreach (GameObject obj in childCubes)
                    {
                        obj.transform.SetParent(pivotObj.transform);
                    }

                    EventManager.TriggerEvent("OnRotateUp", pivotObj);

                    onSliceUp = false;
                    onSliceUpDown = false;
                }
                else if(onSliceUp == false)
                {
                    if(hit.collider.gameObject.transform.parent.name.Equals("Pivot"))
                    {
                        EventManager.TriggerEvent("OnRotateDown", hit.collider.gameObject.transform.parent.gameObject);

                        onSliceUp = false;
                        onSliceUpDown = false;
                        return;
                    }

                    GameObject[] childCubes = FindGameObjectsWithXPosition(hit.point.x);
                    Vector3 center = GetCenter(childCubes);

                    GameObject pivotObj = new GameObject("Pivot");
                    pivotObj.transform.SetParent(rubik.transform);
                    pivotObj.transform.position = center;

                    foreach (GameObject obj in childCubes)
                    {
                        obj.transform.SetParent(pivotObj.transform);
                    }

                    EventManager.TriggerEvent("OnRotateDown", pivotObj);
                    Debug.Log("downnn");

                    onSliceUp = false;
                    onSliceUpDown = false;
                }
                movable = false;
            }
        }
    }
    void OnSliceLeft()
    {
        onSliceLeftRight = true;
        onSliceRight = false;
        onSliceStarted = true;
    }
    void OnSliceRight()
    {
        onSliceLeftRight = true;
        onSliceRight = true;
        onSliceStarted = true;
    }
    void OnSliceUp()
    {
        onSliceUpDown = true;
        onSliceUp = true;
        onSliceStarted = true;
    }
    void OnSliceDown()
    {
        onSliceUpDown = true;
        onSliceUp = false;
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
                returnArr[counter++] = obj;
            }
        }
        return returnArr;
    }
    GameObject[] FindGameObjectsWithXPosition(float xPos){
        GameObject[] returnArr = new GameObject[9];
        int counter = 0;
        foreach (GameObject obj in cubes)
        {
            if (Mathf.Abs(obj.transform.position.x - xPos) < 0.5f) // Check if the y position is close to 0.5
            {
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
