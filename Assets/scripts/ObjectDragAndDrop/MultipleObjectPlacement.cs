using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class MultipleObjectPlacement : MonoBehaviour
{
    [SerializeField] private GameObject informationPopUp;
    [SerializeField] GameObject objectPrefab;
    [SerializeField] Camera _camera;

    private PlacedObject lastSelectedObject;
    private Vector3 touchPosition;
    private ARRaycastManager _raycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private void Start()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
        informationPopUp.SetActive(true);
    }
    private void Update()
    {
        if(informationPopUp != null && informationPopUp.activeSelf) return;

        if (Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);

            touchPosition = _touch.position;

            if (_touch.phase == TouchPhase.Began)
            {
                Ray ray = _camera.ScreenPointToRay(_touch.position);
                RaycastHit raycastHit;
                if (Physics.Raycast(ray, out raycastHit))
                {
                    lastSelectedObject = raycastHit.transform.GetComponent<PlacedObject>();
                    if (lastSelectedObject != null)
                    {
                        PlacedObject[] placedObjects = FindObjectsOfType<PlacedObject>();
                        foreach (PlacedObject obj in placedObjects)
                            obj.IsSelected = obj.GetHashCode() == lastSelectedObject.GetHashCode();
                    }
                }
            }

            if (_touch.phase == TouchPhase.Ended)
            {
                if (lastSelectedObject != null)
                    lastSelectedObject.IsSelected = false;
            }

            if (_raycastManager.Raycast(touchPosition, hits, TrackableType.Planes))
            {
                Pose hitPos = hits[0].pose;
                if (lastSelectedObject == null)
                {
                    GameObject _obj = Instantiate(objectPrefab, hitPos.position, hitPos.rotation);
                    _obj.TryGetComponent<PlacedObject>(out lastSelectedObject);
                }
                else
                {
                    if (lastSelectedObject.IsSelected)
                    {
                        lastSelectedObject.transform.position = hitPos.position;
                        lastSelectedObject.transform.rotation = hitPos.rotation;
                    }
                }
            }
        }
    }
}//MultipleObjectPlacement class end
