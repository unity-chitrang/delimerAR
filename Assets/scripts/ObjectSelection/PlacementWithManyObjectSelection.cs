
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
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    PlacedObject placedObject = hit.transform.GetComponent<PlacedObject>();
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
        for (int i = 0; i < placedObjects.Length; i++) 
        {
            placedObjects[i].SetColor(deActiveColor);
            placedObjects[i].IsSelected = false;
        }
        placedObject.SetColor(activeColor);
        placedObject.IsSelected = true;
    }

}//PlacementWithManyObjectSelection class end
