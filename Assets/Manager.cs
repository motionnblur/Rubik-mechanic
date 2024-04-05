using System.Collections.Generic;
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
    private GameObject pivot;
    void OnEnable()
    {
        EventManager.AddListener("OnMouseDown", OnMouseDown);
        EventManager.AddListener("OnMouseUp", OnMouseUp);
        EventManager.AddListener("OnSliceRight", OnSliceRight);
        EventManager.AddListener("OnSliceLeft", OnSliceLeft);
        EventManager.AddListener("OnSliceUp", OnSliceUp);
        EventManager.AddListener("OnSliceDown", OnSliceDown);
        EventManager.AddListener("OnSliceCenter", OnSliceCenter);
        EventManager.AddListener("OnPivotReset", OnPivotReset);
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
        EventManager.RemoveListener("OnPivotReset", OnPivotReset);
    }
    private void Awake()
    {
        pivot = GameObject.FindWithTag("Pivot");
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
                    GameObject[] childCubes = FindGameObjectsWithYPosition(hit.point.y);
                    Vector3 center = GetCenter(childCubes);

                    pivot.transform.position = center;

                    foreach (GameObject obj in childCubes)
                    {
                        obj.transform.SetParent(pivot.transform);
                    }

                    EventManager.TriggerEvent("OnRotateRight", pivot);

                    onSliceRight = false;
                    onSliceLeftRight = false;
                }
                else
                {
                    GameObject[] childCubes = FindGameObjectsWithYPosition(hit.point.y);
                    Vector3 center = GetCenter(childCubes);

                    pivot.transform.position = center;

                    foreach (GameObject obj in childCubes)
                    {
                        obj.transform.SetParent(pivot.transform);
                    }

                    EventManager.TriggerEvent("OnRotateLeft", pivot);

                    onSliceRight = true;
                    onSliceLeftRight = false;
                }
                    movable = false;
            }
            else
            {
                if (onSliceUp == true)
                {
                    GameObject[] childCubes = FindGameObjectsWithXPosition(hit.point.x);
                    Vector3 center = GetCenter(childCubes);

                    pivot.transform.position = center;

                    foreach (GameObject obj in childCubes)
                    {
                        obj.transform.SetParent(pivot.transform);
                    }

                    EventManager.TriggerEvent("OnRotateUp", pivot);

                    onSliceUp = false;
                    onSliceUpDown = false;
                }
                else
                {
                    GameObject[] childCubes = FindGameObjectsWithXPosition(hit.point.x);
                    Vector3 center = GetCenter(childCubes);

                    pivot.transform.position = center;

                    foreach (GameObject obj in childCubes)
                    {
                        obj.transform.SetParent(pivot.transform);
                    }

                    EventManager.TriggerEvent("OnRotateDown", pivot);

                    onSliceUp = true;
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
        onSliceLeftRight = false;
        onSliceUp = true;
        onSliceStarted = true;
    }
    void OnSliceDown()
    {
        onSliceLeftRight = false;
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
    void OnPivotReset(GameObject pivot)
    {        
        List<GameObject> childCubes = new List<GameObject>();
        for(int i = 0; i < pivot.transform.childCount; i++)
        {
            childCubes.Add(pivot.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < childCubes.Count; i++)
        {
            childCubes[i].transform.SetParent(rubik.transform);
        }
        pivot.transform.rotation = Quaternion.identity;
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
