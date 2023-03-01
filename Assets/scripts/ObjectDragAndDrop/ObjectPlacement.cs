using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class ObjectPlacement : MonoBehaviour
{
    ARRaycastManager raycastManager;

    [SerializeField]
    private GameObject placedPrefab;
    private GameObject placedObject;
    [SerializeField] private Camera camera;

    private List<ARRaycastHit> hitList = new List<ARRaycastHit>();
    private Vector2 touchPos = default;
    private bool onTouchHold;
    private void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = camera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    Debug.Log($"hitobject: {hitObject.transform.name}");
                    if (hitObject.transform.name.Contains("PlacedObject"))
                    {
                        Debug.Log($"hitobject: {hitObject.transform.name} OnTouchHold");
                        onTouchHold = true;
                    }
                }
            }
            else
            {
                if (touch.phase == TouchPhase.Ended) {                     
                    onTouchHold = false;
                    Debug.Log($"hitobject: On release");
                }
            }

            if (raycastManager.Raycast(touchPos, hitList, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                Pose hitPos = hitList[0].pose;

                if (placedObject == null)
                {
                    placedObject = Instantiate(placedPrefab, hitPos.position, hitPos.rotation);
                    Debug.Log($"Object Placed: {placedObject.name}");
                }
                else if (onTouchHold)
                {
                    Debug.Log($"hitobject: DRAG");
                    placedObject.transform.position = hitPos.position;
                    placedObject.transform.rotation = hitPos.rotation;
                }
            }
        }
    }

}//ObjectPlacement class end
