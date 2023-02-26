using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementManager : MonoBehaviour
{
    private ARRaycastManager _arRaycastManager;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    public GameObject _objectToPlace;

    public GameObject ObjectToPlace
    {
        get { return _objectToPlace; }
        set { _objectToPlace = value; }
    }

    bool TryToGetTouchPos(out Vector2 pos)
    {
        if (Input.touchCount > 0)
        {
            pos = Input.GetTouch(0).position;
            return true;
        }
        pos = default;
        return false;
    }
    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (!TryToGetTouchPos(out Vector2 pos)) return;

        if(_arRaycastManager.Raycast(pos,_hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose _hitPos = _hits[0].pose;
            Instantiate(ObjectToPlace, _hitPos.position, _hitPos.rotation);
        }
    }


}//PlacementManager class end
