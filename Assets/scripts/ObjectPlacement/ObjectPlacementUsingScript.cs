using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ObjectPlacementUsingScript : MonoBehaviour
{
    private ARRaycastManager _arRaycastManager;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    private GameObject placedGameObject;

    [SerializeField] private TextMeshProUGUI selectionInformationTxt;

    [SerializeField] private Button redButton;
    [SerializeField] private Button greenButton;
    [SerializeField] private Button blueButton;

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

        ChangePrefabTo("ARBlue");
        redButton.onClick.AddListener(() => { ChangePrefabTo("ARRed");});
        greenButton.onClick.AddListener(() => { ChangePrefabTo("ARGreen");});
        blueButton.onClick.AddListener(() => { ChangePrefabTo("ARBlue");});
    }

    void ChangePrefabTo(string prefabName)
    {
        string path = $"prefabs/{prefabName}";
        placedGameObject = Resources.Load<GameObject>(path);
        if (placedGameObject == null)
            Debug.LogError($"ERROR: Placed Prefab is Null with the given Name: {prefabName}");
        else
            selectionInformationTxt.text = $"Slelected: {prefabName}";
    }

    private void Update()
    {
        if (!TryToGetTouchPos(out Vector2 pos) || placedGameObject == null) return;

        if (_arRaycastManager.Raycast(pos, _hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose _hitPos = _hits[0].pose;
            Instantiate(placedGameObject, _hitPos.position, _hitPos.rotation);
        }
    }

}
