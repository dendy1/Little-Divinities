using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private void Update()
    {
        if (transform.GetComponent<Camera>().fieldOfView <= 60 && transform.GetComponent<Camera>().fieldOfView >= 12)
        {
            transform.GetComponent<Camera>().fieldOfView += -(Input.GetAxis("Mouse ScrollWheel")) * 7;
        }

        if (transform.GetComponent<Camera>().fieldOfView > 60)
            transform.GetComponent<Camera>().fieldOfView = 60;

        if (transform.GetComponent<Camera>().fieldOfView < 12)
            transform.GetComponent<Camera>().fieldOfView = 12;
    }
}
