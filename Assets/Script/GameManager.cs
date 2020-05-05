using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [Header("TEXT FIELDS")]
    public Text spheresText_1;
    public Text resourcesText;

    public TMP_Text spheresText;
    public TMP_Text woodText;
    public TMP_Text stoneText;
    public TMP_Text waterText;
    public TMP_Text energyText;

    [Header("RESOURCES")]
    public float WoodCount;
    public float StoneCount;
    public float EnergyCount;
    public float WaterCount;
    public float CrystalCount;
    
    [Header("PEOPLE STATISTICS")]
    public float Food;
    public float Health;
    public float Happinnes;
    public float Ecomony;

    private List<IslandController> _islandControllers;
    private IslandController _vacationIslandController;    
    private IslandController _mergeIslandController;

    public GameObject deathParticles;
    public bool PopupMenuOpened { get; set; }

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    private void Start()
    {
        _islandControllers = FindObjectsOfType<IslandController>().ToList();
        _vacationIslandController = GameObject.FindGameObjectWithTag("VacationIsland").GetComponent<IslandController>();
        _mergeIslandController = GameObject.FindGameObjectWithTag("MergeIsland").GetComponent<IslandController>();
        
        TurnOffAllOutline();

        CrystalCount = 500;
        WoodCount = 100;
        StoneCount = 100;
        WaterCount = 100;
        EnergyCount = 100;
        
        spheresText.text = "" + (int)CrystalCount;
        woodText.text = "" + (int)WoodCount;
        stoneText.text = "" + (int)StoneCount;
        waterText.text = "" + (int)WaterCount;
        energyText.text = "" + (int)EnergyCount;
    }

    public void TurnOffAllOutline()
    {
        foreach (var islandController in _islandControllers)
        {
            islandController.TurnOutline(false);
        }
    }

    public void TurnVacationIslandOutline()
    {
        _vacationIslandController.TurnOutline(true);
    }
    
    public void TurnMergeIslandOutline()
    {
        _mergeIslandController.TurnOutline(true);
    }

    public bool BuyMinion(float cost)
    {
        if (CrystalCount - cost < 0)
        {
            return false;
        }

        CrystalCount -= cost;
        spheresText.text = "" + (int)CrystalCount;
        woodText.text = "" + (int)WoodCount;
        stoneText.text = "" + (int)StoneCount;
        waterText.text = "" + (int)WaterCount;
        energyText.text = "" + (int)EnergyCount;
        return true;
    }

    public void Harvest(MinionDescriptor.Type type, float power)
    {
        switch (type)
        {
            case MinionDescriptor.Type.Forester:
                WoodCount += power;
                break;
            case MinionDescriptor.Type.Stoner:
                StoneCount += power;
                break;
            case MinionDescriptor.Type.Waterman:
                WaterCount += power;
                break;
            case MinionDescriptor.Type.Energyman:
                EnergyCount += power;
                break;
            case MinionDescriptor.Type.Crystalman:
                CrystalCount += power;
                break;
        }

        spheresText.text = "" + (int)CrystalCount;
        woodText.text = "" + (int)WoodCount;
        stoneText.text = "" + (int)StoneCount;
        waterText.text = "" + (int)WaterCount;
        energyText.text = "" + (int)EnergyCount;
    }

    private void Update()
    {
        spheresText.text = "" + (int)CrystalCount;
        woodText.text = "" + (int)WoodCount;
        stoneText.text = "" + (int)StoneCount;
        waterText.text = "" + (int)WaterCount;
        energyText.text = "" + (int)EnergyCount;

        if (CrystalCount < 0) CrystalCount = 0;
        if (WoodCount < 0) WoodCount = 0;
        if (StoneCount < 0) StoneCount = 0;
        if (WaterCount < 0) WaterCount = 0;
        if (EnergyCount < 0) EnergyCount = 0;
    }

    public void DisableButton(Button button)
    {
        
    }
}
