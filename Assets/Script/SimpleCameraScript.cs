using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraScript : MonoBehaviour
{
    public float movementSpeed;
    
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        
        transform.Translate(horizontal, 0f, vertical);
        //transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
