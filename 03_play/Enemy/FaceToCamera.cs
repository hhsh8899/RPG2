using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
    private Camera camLookAt;
    // Start is called before the first frame update
    void Start()
    {
        camLookAt = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = camLookAt.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(camLookAt.transform.position - v);
        
    }
}
