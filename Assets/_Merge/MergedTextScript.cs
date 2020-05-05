using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergedTextScript : MonoBehaviour
{

    private Vector3 _scale;

    private Vector3 _targetPosition;



    IEnumerator Animation()
    {
        yield return new WaitForSeconds(1f);
        _scale *= 1.5f;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void Awake()
    {
        _targetPosition = gameObject.transform.position;
        _scale = transform.localScale;
        _targetPosition += new Vector3(0, 3, 0);
        StartCoroutine(Animation());
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _scale, 1f * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, _targetPosition, 1f * Time.deltaTime);
        transform.LookAt(Camera.main.transform);        
    }
}
