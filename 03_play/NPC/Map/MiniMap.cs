using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private Camera cam;
 
    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag(Tags.miniMap).GetComponent<Camera>();
    }
    public void OnZoomIn()
    {
        cam.orthographicSize--;
    }
    public void OnZoomOut()
    {
        cam.orthographicSize++;
    }
}
