using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class CameraMouseRotator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float _xStartCoord;
    private bool _isMoving;
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (!_isMoving)
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _isMoving = true;
                _xStartCoord = Screen.width * 0.5f; //Input.mousePosition.magnitude;
            }
        
        if (_isMoving && Input.GetKey(KeyCode.Mouse1))
        {
            transform.Rotate( new Vector3(0,1,0), (Input.mousePosition.x - _xStartCoord)/-350f);
        }

        if (Input.GetKeyUp((KeyCode.Mouse1)))
        {
            _isMoving = false;
        }
        
    }
}
