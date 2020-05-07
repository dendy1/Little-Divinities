using TMPro;
using UnityEngine;

public class InterfaceManager : MonoBehaviourSingleton<InterfaceManager>
{
    [SerializeField] private TMP_Text crystalText;
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text stoneText;
    [SerializeField] private TMP_Text waterText;
    [SerializeField] private TMP_Text powerText;

    public void UpdateTextFields()
    {
        crystalText.text = "" + GameManager.Instance.CrystalCount;
        woodText.text = "" + GameManager.Instance.WoodCount;
        stoneText.text = "" + GameManager.Instance.StoneCount;
        waterText.text = "" + GameManager.Instance.WaterCount;
        powerText.text = "" + GameManager.Instance.PowerCount;
    }

    public void UpdateCrystalTextField()
    {
        crystalText.text = "" + GameManager.Instance.CrystalCount;
    }
    
    public void UpdateWoodTextField()
    {
        woodText.text = "" + GameManager.Instance.WoodCount;
    }
    
    public void UpdateStoneTextField()
    {
        stoneText.text = "" + GameManager.Instance.StoneCount;
    }
    
    public void UpdateWaterTextField()
    {
        waterText.text = "" + GameManager.Instance.WaterCount;
    }
    
    public void UpdatePowerTextField()
    {
        powerText.text = "" + GameManager.Instance.PowerCount;
    }
}
