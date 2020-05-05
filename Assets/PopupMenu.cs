using System;
using UnityEngine;

public class PopupMenu : MonoBehaviour
{
    [SerializeField] private PopupMenuController popupMenuController;
    [SerializeField] private int minionsUpdateCost;
    [SerializeField] private int resourcesUpdateCost;
    
    [SerializeField] private int minionsMaxIncreaseValue;
    [SerializeField] private int resourcesMaxIncreaseValue;
    
    [SerializeField] private int minionsUpdateCostIncreaseValue;
    [SerializeField] private int resourcesUpdateCostIncreaseValue;

    private IslandController _controller;

    private void Start()
    {
        _controller = GetComponent<IslandController>();
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.PopupMenuOpened || CompareTag("VacationIsland") || CompareTag("MergeIsland"))
            return;
        
        popupMenuController.gameObject.SetActive(true);
        GameManager.Instance.PopupMenuOpened = true;
        UpdateTextFields();
    }

    public void BuyResourceUpgrade()
    {
        if (GameManager.Instance.CrystalCount - resourcesUpdateCost < 0)
            return;

        GameManager.Instance.CrystalCount -= resourcesUpdateCost;
        _controller.maxResources += resourcesMaxIncreaseValue;
        resourcesUpdateCost += resourcesUpdateCostIncreaseValue;

        popupMenuController.currentMaxResources.text = "Макс - " + _controller.maxResources;
        popupMenuController.nextMaxResources.text = "Макс - " + (_controller.maxResources + resourcesMaxIncreaseValue);
        popupMenuController.resourcesUpdateCost.text = resourcesUpdateCost.ToString();
    }

    public void BuyMinionsUpgrade()
    {
        if (GameManager.Instance.CrystalCount - minionsUpdateCost < 0)
            return;

        GameManager.Instance.CrystalCount -= minionsUpdateCost;
        _controller.maxMinions += minionsMaxIncreaseValue;
        minionsUpdateCost += minionsUpdateCostIncreaseValue;
        
        popupMenuController.currentMaxMinions.text = "Макс - " + _controller.maxMinions;
        popupMenuController.nextMaxMinions.text = "Макс - " + (_controller.maxMinions + minionsMaxIncreaseValue);
        popupMenuController.minionsUpdateCost.text = minionsUpdateCost.ToString();
    }

    public void BuyRainResist()
    {
        if (GameManager.Instance.CrystalCount - 50 < 0 || _controller.rainUpgrade)
            return;

        _controller.rainUpgrade = true;
        _controller.rainObject.SetActive(true);
        foreach (var catastrophe in _controller.catastropheList)
        {
            if (catastrophe is RainCatastrophe)
            {
                _controller.catastropheList.Remove(catastrophe);
                break;
            }
        }
        GameManager.Instance.CrystalCount -= 50;
    }
    
    public void BuyFireResist()
    {
        if (GameManager.Instance.CrystalCount - 50 < 0 || _controller.fireUpgrade)
            return;
        
        _controller.fireUpgrade = true;
        _controller.fireObject.SetActive(true);
        foreach (var catastrophe in _controller.catastropheList)
        {
            if (catastrophe is FireCatastrophe)
            {
                _controller.catastropheList.Remove(catastrophe);
                break;
            }
        }
        GameManager.Instance.CrystalCount -= 50;
    }
    
    public void BuyStormResist()
    {
        if (GameManager.Instance.CrystalCount - 50 < 0 || _controller.stormUpgrade)
            return;
        
        _controller.stormUpgrade = true;
        _controller.stormObject.SetActive(true);
        foreach (var catastrophe in _controller.catastropheList)
        {
            if (catastrophe is StormCatastrophe)
            {
                _controller.catastropheList.Remove(catastrophe);
                break;
            }
        }
        GameManager.Instance.CrystalCount -= 50;
    }

    private void UpdateTextFields()
    {
        popupMenuController.currentMaxMinions.text = "Макс - " + _controller.maxMinions;
        popupMenuController.currentMaxResources.text = "Макс - " + _controller.maxResources;
        
        popupMenuController.nextMaxMinions.text = "Макс - " + (_controller.maxMinions + minionsMaxIncreaseValue);
        popupMenuController.nextMaxResources.text = "Макс - " + (_controller.maxResources + resourcesMaxIncreaseValue);
        
        popupMenuController.minionsUpdateCost.text = minionsUpdateCost.ToString();
        popupMenuController.resourcesUpdateCost.text = resourcesUpdateCost.ToString();
    }
}
