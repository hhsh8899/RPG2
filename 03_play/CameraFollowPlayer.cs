using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;
    public float scrollSpeed = 10;
    public float distance = 0;
    private bool isRotating=false;
    public float rotateSpeed = 1;
   
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        transform.LookAt(player);
       
        offset = transform.position - player.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offset + player.position;
    
       
        RorateView();//控制视野方向旋转
        ScrollView();//控制视野拉近拉远 
    }
    void ScrollView()
    {
        distance = offset.magnitude;
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance = Mathf.Clamp(distance, 2,15);
        offset = offset.normalized * distance;
    }
    void RorateView()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }
        if (isRotating)
        {
            transform.RotateAround(player.position, player.up, Input.GetAxis("Mouse X") * rotateSpeed);
            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;
            transform.RotateAround(player.position, transform.right, -Input.GetAxis("Mouse Y") * rotateSpeed);
            float x = transform.eulerAngles.x;
            if (x < 10 || x > 80)
            {
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }
            offset = transform.position - player.position;
        }
        
    }
}
