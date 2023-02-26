using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    public bool IsSelected = false;
        
    public void SetColor(Color color) 
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
}
