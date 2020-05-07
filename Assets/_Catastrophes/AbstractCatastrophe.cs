using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCatastrophe : MonoBehaviour
{
    [SerializeField] protected List<BaseScriptableEffect> effects;
    [SerializeField] protected IslandController islandController;
    
    [SerializeField] protected float duration;
    protected bool IsEnabled;
    protected Delay Timer;

    private void Awake()
    {
        Timer = new Delay(duration);
    }

    public void StartCatastrophe()
    {
        IsEnabled = true;
        islandController.catastropheEnabled = true;
        Timer.Reset();
    }
}
