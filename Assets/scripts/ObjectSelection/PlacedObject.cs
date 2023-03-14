using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Android;

public class PlacedObject : MonoBehaviour
{
    private bool _IsSelected { get; set; }

    public bool IsSelected
    {
        get => _IsSelected;
        set => _IsSelected = value;
    }
    private MeshRenderer _MeshRenderer;

    [SerializeField] private TextMeshPro overlayText;

    [SerializeField] private string overlayName;

    public void Awake()
    {
        if (overlayText != null)
        {
            overlayText.text = overlayName = this.gameObject.name;
            overlayText.enabled = false;
        }
    }

    public void SetColor(Color color)
    {
        GetMeshRenderer().material.color = color;
    }

    MeshRenderer GetMeshRenderer()
    {
        if (_MeshRenderer == null)
            TryGetComponent<MeshRenderer>(out _MeshRenderer);

        return this._MeshRenderer;
    }

    public void DisableOverlayObject()
    {
        overlayText.enabled = false;
    }

    public void ShowObjectInformation()
    {
        if (overlayText != null && string.IsNullOrEmpty(overlayText.text))
            overlayText.text = overlayName;

        overlayText.enabled = true;
    }
}//PlacedObject class end.
