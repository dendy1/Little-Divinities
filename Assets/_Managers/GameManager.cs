using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    #region Resource fields

    private float _woodCount;
    public float WoodCount
    {
        get => _woodCount;
        set
        {
            _woodCount = value;
            InterfaceManager.Instance.UpdateWoodTextField();
        }
    }

    private float _stoneCount;
    public float StoneCount { 
        get => _stoneCount;
        set
        {
            _stoneCount = value;
            InterfaceManager.Instance.UpdateStoneTextField();
        }
    }
    
    private float _powerCount;
    public float PowerCount { 
        get => _powerCount;
        set
        {
            _powerCount = value;
            InterfaceManager.Instance.UpdatePowerTextField();
        }
    }
    
    private float _waterCount;
    public float WaterCount { 
        get => _waterCount;
        set
        {
            _waterCount = value;
            InterfaceManager.Instance.UpdateWaterTextField();
        }
    }
    
    private float _crystalCount;
    public float CrystalCount { 
        get => _crystalCount;
        set
        {
            _crystalCount = value;
            InterfaceManager.Instance.UpdateCrystalTextField();
        }
    }

    #endregion

    #region Human stats fields

    public float Food { get; set; }
    public float Health { get; set; }
    public float Happiness { get; set; }
    public float Economy { get; set; }

    #endregion

    [Header("Resource Island Controllers")]
    public List<IslandController> islandControllers;

    [Header("Special Island Controllers")]
    public IslandController vacationIslandController;    
    public IslandController mergeIslandController;
    
    [Header("FX Components")]
    public GameObject deathParticles;
    
    public bool PopupMenuOpened { get; set; }

    private void Start()
    {
        SetActiveIslandsOutline(false);

        CrystalCount = 500;
        WoodCount = 100;
        StoneCount = 100;
        WaterCount = 100;
        CrystalCount = 100;
    }

    public void SetActiveIslandsOutline(bool active)
    {
        foreach (var islandController in islandControllers)
        {
            islandController.SetActiveOutline(active);
        }

        SetActiveVacationIslandOutline(active);
        SetActiveMergeIslandOutline(active);
    }

    public void SetActiveVacationIslandOutline(bool active)
    {
        vacationIslandController.SetActiveOutline(active);
    }
    
    public void SetActiveMergeIslandOutline(bool active)
    {
        mergeIslandController.SetActiveOutline(active);
    }

    public void Harvest(ResourceType resourceType, float value)
    {
        switch (resourceType)
        {
            case ResourceType.Wood:
                WoodCount += value;
                break;
            
            case ResourceType.Stone:
                StoneCount += value;
                break;
            
            case ResourceType.Water:
                WaterCount += value;
                break;
            
            case ResourceType.Power:
                PowerCount += value;
                break;
            
            case ResourceType.Shards:
                CrystalCount += value;
                break;
        }
    }
}
