using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviourSingleton<InterfaceManager>
{
    [Header("Resources Text Fields")]
    [SerializeField] private TMP_Text crystalText;
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text stoneText;
    [SerializeField] private TMP_Text waterText;
    [SerializeField] private TMP_Text powerText;

    [Header("Human stats bars")]
    [SerializeField] private Image economyBar;
    [SerializeField] private Image foodBar;
    [SerializeField] private Image happinessBar;
    [SerializeField] private Image healthBar;

    [Header("Pause UI")] 
    [SerializeField] private Canvas pauseCanvas;
    
    public bool PopupMenuOpened { get; set; }
    public bool Paused { get; set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                OnResumeButtonClicked();
            }
            else
            {
                pauseCanvas.gameObject.SetActive(true);
                Time.timeScale = 0;
                Paused = PopupMenuOpened = true;
            }
        }
    }

    public void OnResumeButtonClicked()
    {
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        Paused = PopupMenuOpened = false;
    }
    
    public void OnQuitButtonClicked()
    {
        SceneManager.LoadScene("Menu");
    }

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

    public void UpdateFoodBar()
    {
        foodBar.fillAmount = 0.5f + GameManager.Instance.Food / 20f;
    }

    public void UpdateEconomyBar()
    {
        economyBar.fillAmount = 0.5f + GameManager.Instance.Economy / 20f;
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = 0.5f + GameManager.Instance.Health / 20f;
    }

    public void UpdateHappinessBar()
    {
        happinessBar.fillAmount = 0.5f + GameManager.Instance.Happiness / 20f;
    }
}
