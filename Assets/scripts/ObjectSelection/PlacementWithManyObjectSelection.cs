
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlacementWithManyObjectSelection : MonoBehaviour
{
    [SerializeField]
    PlacedObject[] placedObjects;

    [SerializeField]
    private Color activeColor = Color.green;

    [SerializeField]
    private Color deActiveColor = Color.gray;

    [SerializeField]
    Camera arCamera;

    private void Awake()
    {
        placedObjects = GetComponentsInChildren<PlacedObject>();
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Debug.Log(Input.touchCount);
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                Debug.Log(touch.phase);
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    PlacedObject placedObject = hit.transform.GetComponent<PlacedObject>();
                    Debug.Log(placedObject.name);
                    if(placedObject != null)
                    {
                        ChangeSelectedObject(placedObject);
                    }
                }
            }
        }
    }

    void ChangeSelectedObject(PlacedObject placedObject)
    {
        Debug.Log(placedObject.name + " <<-START");
        for (int i = 0; i < placedObjects.Length; i++) 
        {
            placedObjects[i].SetColor(deActiveColor);
            placedObjects[i].IsSelected = false;
        }
        Debug.Log(placedObject.name + " END-->>");
        placedObject.SetColor(activeColor);
        placedObject.IsSelected = true;
    }

}//PlacementWithManyObjectSelection class end
