using System;
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

    #region Human Stats

    private int _food;
    public int Food
    {
        get => _food;
        set
        {
            _food = Mathf.Clamp(value, -10, 10);
            InterfaceManager.Instance.UpdateFoodBar();
        }
    }
    
    private int _health;
    public int Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, -10, 10);
            InterfaceManager.Instance.UpdateHealthBar();
        }
    }
    
    private int _economy;
    public int Economy
    {
        get => _economy;
        set
        {
            _economy = Mathf.Clamp(value, -10, 10);
            InterfaceManager.Instance.UpdateEconomyBar();
        }
    }
    
    private int _happiness;
    public int Happiness
    {
        get => _happiness;
        set
        {
            _happiness = Mathf.Clamp(value, -10, 10);
            InterfaceManager.Instance.UpdateHappinessBar();
        }
    }

    #endregion
    
    
    [Header("Resource Island Controllers")]
    public List<IslandController> islandControllers;

    [Header("Special Island Controllers")]
    public IslandController vacationIslandController;    
    public IslandController mergeIslandController;
    
    [Header("FX Components")]
    public GameObject deathParticles;
    
    private void Start()
    {
        SetActiveIslandsOutline(false);

        CrystalCount = 500;
        WoodCount = StoneCount = WaterCount = PowerCount = 1000;
        Economy = Food = Health = Happiness = 7;
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
