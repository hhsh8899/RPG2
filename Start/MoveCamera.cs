using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float speed = 2;
    public float changeTime =5.0f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;
        if(timer>0)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

          
        
    }
}
