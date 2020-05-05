using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeVisualize : MonoBehaviour
{

    [Header("Visualization Settings")]

    [SerializeField] GameObject _fxPrefab;

    [SerializeField] float _mergeSpeed = 1f;

    [SerializeField] public GameObject _mergingObj1;

    [SerializeField] public GameObject _mergingObj2;

    [SerializeField] GameObject _onMergedText;

    [SerializeField] float _mergeDistance = .3f;

    private Vector3 _targetCoord;


    private Vector3 CalculateMidPoint()
    {
        if (_mergingObj1 != null && _mergingObj2 != null)

        return (_mergingObj1.transform.position + _mergingObj2.transform.position) / 2f;

        return Vector3.zero;
    }

    private void MergeFX()
    {
        Destroy(_mergingObj1);
        Destroy(_mergingObj2);

        //Здесь создать нового челика и занести в список


        Instantiate(_onMergedText, _targetCoord, Quaternion.identity);
        Instantiate(_fxPrefab, _targetCoord, Quaternion.identity);
        Debug.Log("Merged !");
        gameObject.SetActive(false);
        return;
    }


    // Start is called before the first frame update
    void Awake()
    {
        _targetCoord = CalculateMidPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_mergingObj1.transform.position, _mergingObj2.transform.position) <= _mergeDistance)
        {
            MergeFX();
        }

        _mergingObj1.transform.position = Vector3.Lerp(_mergingObj1.transform.position, _targetCoord, _mergeSpeed * Time.deltaTime);
        _mergingObj2.transform.position = Vector3.Lerp(_mergingObj2.transform.position, _targetCoord, _mergeSpeed * Time.deltaTime);
    }
}
