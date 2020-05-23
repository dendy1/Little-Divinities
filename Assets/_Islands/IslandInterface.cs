using System;
using UnityEngine;

[RequireComponent(typeof(IslandController))]
public class IslandInterface : MonoBehaviour
{
    public bool withoutIcon;
    public GameObject resourceImageGameObject;
    private AnimationState _animationState;

    private IslandController _islandController;

    private void Awake()
    {
        _islandController = GetComponent<IslandController>();
        _animationState = resourceImageGameObject.GetComponent<Animation>()["islandIconAnimation"];
    }

    private void Update()
    {
        if (!resourceImageGameObject) return;
        
        if (withoutIcon || _islandController.MinionsCount < 1)
        {
            resourceImageGameObject.SetActive(false);
        }
        else
        {
            _animationState.speed = _islandController.WorkingMinions;
            resourceImageGameObject.SetActive(true);
        }
    }
}
