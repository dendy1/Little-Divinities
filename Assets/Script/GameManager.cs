using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    #region Resource fields

    public float WoodCount { get; set; }
    public float StoneCount { get; set; }
    public float PowerCount { get; set; }
    public float WaterCount { get; set; }
    public float CrystalCount { get; set; }

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

    public void Harvest(ResourceType resourceType, float power)
    {
        switch (resourceType)
        {
            case ResourceType.Wood:
                WoodCount += power;
                InterfaceManager.Instance.UpdateWoodTextField();
                break;
            
            case ResourceType.Stone:
                StoneCount += power;
                InterfaceManager.Instance.UpdateStoneTextField();
                break;
            
            case ResourceType.Water:
                WaterCount += power;
                InterfaceManager.Instance.UpdateWaterTextField();
                break;
            
            case ResourceType.Power:
                CrystalCount += power;
                InterfaceManager.Instance.UpdatePowerTextField();
                break;
            
            case ResourceType.Shards:
                CrystalCount += power;
                InterfaceManager.Instance.UpdateCrystalTextField();
                break;
        }
    }
}
