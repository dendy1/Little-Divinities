using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class SituationsController : MonoBehaviour
{
    [Header("Situations List")] [SerializeField]
    private List<SituationScriptable> situations;

    [Header("PopUp GameObjects")]
    [SerializeField] private GameObject startSituationPopup;
    [SerializeField] private GameObject situationPopup;
    [SerializeField] private GameObject resultPopup;
    [SerializeField] private GameObject skipPopup;
    
    [Header("PopUp UI TextFields")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text goodText;
    [SerializeField] private TMP_Text badText;

    [Header("PopUp UI Resources TextFields")]
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text stoneText;
    [SerializeField] private TMP_Text powerText;
    [SerializeField] private TMP_Text waterText;

    [Header("PopUp Resources GameObjects")] 
    [SerializeField] private GameObject woodRes;
    [SerializeField] private GameObject stoneRes;
    [SerializeField] private GameObject powerRes;
    [SerializeField] private GameObject waterRes;
    
    [Header("PopUp UI Human Stats TextFields")]
    [SerializeField] private TMP_Text economyText;
    [SerializeField] private TMP_Text foodText;
    [SerializeField] private TMP_Text happinessText;
    [SerializeField] private TMP_Text healthText;

    [Header("PopUp Human Stats GameObjects")] 
    [SerializeField] private GameObject economyRes;
    [SerializeField] private GameObject foodRes;
    [SerializeField] private GameObject happinessRes;
    [SerializeField] private GameObject healthRes;

    [Header("Situation Settings")] 
    [SerializeField] private float ignoreTime = 10f;
    
    private int _situationId;
    private Delay _situationDelay;
    private Delay _skipDelay;

    [SerializeField] private float minTime = 30f;
    [SerializeField] private float maxTime = 50f;
    
    private GameManager GameManager => GameManager.Instance;
    private SituationScriptable CurrentSituation => situations[_situationId];

    private void Awake()
    {
        _situationDelay = new Delay(10f);
        _skipDelay = new Delay(ignoreTime, true);
    }

    private void Start()
    {
        _situationId = 0;

        situationPopup.SetActive(false);
        skipPopup.SetActive(false);
    }

    private void Update()
    {
        if (_situationId >= situations.Count)
            return;
        
        if (_situationDelay.IsReady)
        {
            startSituationPopup.gameObject.SetActive(true);
            _skipDelay.Reset();
            _situationDelay.Stop();
        }

        if (_skipDelay.IsReady)
        {
            SkipSituation();
            _skipDelay.Stop();
        }
    }

    private void GoodChoicePrice()
    {
        GameManager.WoodCount -= CurrentSituation.goodChoiceWood;
        GameManager.PowerCount -= CurrentSituation.goodChoicePower;
        GameManager.StoneCount -= CurrentSituation.goodChoiceStone;
        GameManager.CrystalCount -= CurrentSituation.goodChoiceCrystal;
        GameManager.WaterCount -= CurrentSituation.goodChoiceWater;
    }

    private void BadChoicePrice()
    {
        GameManager.WoodCount -= CurrentSituation.badChoiceWood;
        GameManager.PowerCount -= CurrentSituation.badChoicePower;
        GameManager.StoneCount -= CurrentSituation.badChoiceStone;
        GameManager.CrystalCount -= CurrentSituation.badChoiceCrystal;
        GameManager.WaterCount -= CurrentSituation.badChoiceWater;
    }

    private void GoodChoiceResult()
    {
        GameManager.Economy += CurrentSituation.goodChoiceEconomy;
        GameManager.Health += CurrentSituation.goodChoiceHealth;
        GameManager.Happiness += CurrentSituation.goodChoiceHappiness;
        GameManager.Food += CurrentSituation.goodChoiceFood;
        
        _situationId++;
        _situationDelay.WaitTime = Random.Range(minTime, maxTime);
        _situationDelay.Reset();
        
        Time.timeScale = 1;
        situationPopup.SetActive(false);
    }

    private void BadChoiceResult()
    {
        GameManager.Economy += CurrentSituation.badChoiceEconomy;
        GameManager.Health += CurrentSituation.badChoiceHealth;
        GameManager.Happiness += CurrentSituation.badChoiceHappiness;
        GameManager.Food += CurrentSituation.badChoiceFood;
        
        _situationId++;
        _situationDelay.WaitTime = Random.Range(minTime, maxTime);
        _situationDelay.Reset();
        
        Time.timeScale = 1;
        situationPopup.SetActive(false);
    }
    
    private void IgnoreChoiceResult()  // Если игрок выбрал "Ничего не делать"
    {
        _situationId++;
        _situationDelay.WaitTime = Random.Range(minTime, maxTime);
        _situationDelay.Reset();
        
        Time.timeScale = 1;
        situationPopup.SetActive(false);
        
        skipPopup.SetActive(true);
        Penalty();
    }
    
    private void SkipSituation()  // Если игрок не успел ничего выбрать
    {
        _situationDelay.WaitTime = Random.Range(80, 140);
        _situationDelay.Reset();
        
        startSituationPopup.gameObject.SetActive(false);
        skipPopup.SetActive(true);
        Penalty();
    }
    
    private void Penalty()
    {
        GameManager.WoodCount -= 10;
        GameManager.StoneCount -= 10;
        GameManager.PowerCount -= 10;
        GameManager.WaterCount -= 10;
    }

    #region UI Event Handlers

    public void OnStartSituationClicked()
    {
        nameText.text = CurrentSituation.name;
        descriptionText.text = CurrentSituation.description;
        goodText.text = CurrentSituation.goodChoice;
        badText.text = CurrentSituation.badChoice;
        
        startSituationPopup.SetActive(false);
        situationPopup.SetActive(true);
        Time.timeScale = 0;
        
        _situationDelay.Stop();
        _skipDelay.Stop();
    } 

    public void OnGoodChoiceClicked()
    {
        GoodChoicePrice();
        GoodChoiceResult();
    }

    public void OnBadChoiceClicked()
    {
        BadChoicePrice();
        BadChoiceResult();
    }

    public void OnSkipChoiceClicked()
    {
        IgnoreChoiceResult();
    }
    
    public void OnEnterGoodChoiceButton() // Если игрок навёл на кнопку с хорошим выбором
    {
        if (CurrentSituation.goodChoicePower != 0)
        {
            powerRes.SetActive(true); 
            powerText.text = CurrentSituation.goodChoicePower.ToString();
        }
        
        if (CurrentSituation.goodChoiceWood != 0)
        {
            woodRes.SetActive(true); 
            woodText.text = CurrentSituation.goodChoiceWood.ToString();
        }
        
        if (CurrentSituation.goodChoiceWater != 0)
        {
            waterRes.SetActive(true); 
            waterText.text = CurrentSituation.goodChoiceWater.ToString();
        }
        
        if (CurrentSituation.goodChoiceStone != 0)
        {
            stoneRes.SetActive(true); 
            stoneText.text = CurrentSituation.goodChoiceStone.ToString();
        }

        if (CurrentSituation.goodChoiceEconomy != 0)
        {
            economyRes.SetActive(true);
            economyText.text = CurrentSituation.goodChoiceEconomy.ToString();
        }
        
        if (CurrentSituation.goodChoiceHealth != 0)
        {
            healthRes.SetActive(true);
            healthText.text = CurrentSituation.goodChoiceHealth.ToString();
        }
        
        if (CurrentSituation.goodChoiceHappiness != 0)
        {
            happinessRes.SetActive(true);
            happinessText.text = CurrentSituation.goodChoiceHappiness.ToString();
        }
        
        if (CurrentSituation.goodChoiceFood != 0)
        {
            foodRes.SetActive(true);
            foodText.text = CurrentSituation.goodChoiceFood.ToString();
        }
    }

    public void OnEnterBadChoiceButton() // Если игрок навёл на кнопку с плохим выбором
    {
        if (CurrentSituation.badChoicePower != 0)
        {
            powerRes.SetActive(true); 
            powerText.text = CurrentSituation.badChoicePower.ToString();
        }
        
        if (CurrentSituation.badChoiceWood != 0)
        {
            woodRes.SetActive(true); 
            woodText.text = CurrentSituation.badChoiceWood.ToString();
        }
        
        if (CurrentSituation.badChoiceWater != 0)
        {
            waterRes.SetActive(true); 
            waterText.text = CurrentSituation.badChoiceWater.ToString();
        }
        
        if (CurrentSituation.badChoiceStone != 0)
        {
            stoneRes.SetActive(true); 
            stoneText.text = CurrentSituation.badChoiceStone.ToString();
        }

        if (CurrentSituation.badChoiceEconomy != 0)
        {
            economyRes.SetActive(true);
            economyText.text = CurrentSituation.badChoiceEconomy.ToString();
        }
        
        if (CurrentSituation.badChoiceHealth != 0)
        {
            healthRes.SetActive(true);
            healthText.text = CurrentSituation.badChoiceHealth.ToString();
        }
        
        if (CurrentSituation.badChoiceHappiness != 0)
        {
            happinessRes.SetActive(true);
            happinessText.text = CurrentSituation.badChoiceHappiness.ToString();
        }
        
        if (CurrentSituation.badChoiceFood != 0)
        {
            foodRes.SetActive(true);
            foodText.text = CurrentSituation.badChoiceFood.ToString();
        }
    }

    public void OnExitChoiceButton()  //Если игрок убрал курсор с любой кнопки выбора
    {
        woodRes.SetActive(false);
        stoneRes.SetActive(false);
        waterRes.SetActive(false);
        powerRes.SetActive(false);
        
        foodRes.SetActive(false);
        healthRes.SetActive(false);
        economyRes.SetActive(false);
        happinessRes.SetActive(false);
    }

    public void OnCloseButtonClicked(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    #endregion
}
