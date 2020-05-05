using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAnimation : MonoBehaviour
{
    [SerializeField] GameObject CenterCloud;

    [SerializeField] GameObject RoundingClouds;

    // Update is called once per frame
    void Update()
    {
        RoundingClouds.transform.Rotate(new Vector3(0, 1, 0), .1f);
    }
}
