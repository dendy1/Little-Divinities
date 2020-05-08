using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayController : MonoBehaviour
{
    public int currentDay;
        
    [Header("Settings for day length")] 
    [SerializeField] private int minutes = 10;
    [SerializeField] private int seconds = 0;

    [Header("Desired resources for day")] 
    [SerializeField] private float desiredWood;
    [SerializeField] private float desiredStone;
    [SerializeField] private float desiredPower;
    [SerializeField] private float desiredWater;

    [Header("GameLogic PopUp GameObjects")] 
    [SerializeField] private GameObject popupDayInfo;

    [Header("Start Day PopUp UI Resources GameObjects")] 
    [SerializeField] private GameObject woodRes;
    [SerializeField] private GameObject stoneRes;
    [SerializeField] private GameObject powerRes;
    [SerializeField] private GameObject waterRes;

    [Header("Start Day PopUp UI Resources TextFields")] 
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text stoneText;
    [SerializeField] private TMP_Text powerText;
    [SerializeField] private TMP_Text waterText;
    
    [Header("Start Day PopUp UI TextFields")] 
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;

    [Header("Day/Time TextField for UI")] 
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private TMP_Text timeText;
    
    private GameManager GameManager => GameManager.Instance;
    private Delay _dayDelay;

    private const int SECONDS_IN_MINUTE = 60;

    private void Awake()
    {
        _dayDelay = new Delay(minutes * SECONDS_IN_MINUTE + seconds, true);
    }

    private void Start()
    {
        currentDay = 1;
        timeText.text = "";
        dayText.text = "";
        
        SetStartDayData();
        popupDayInfo.SetActive(true);

        GameManager.Instance.PopupMenuOpened = true;
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (_dayDelay.IsReady)
        {
            DayEnded();
            _dayDelay.Stop();
        }
    }

    private void DayEnded()
    {
        SetEndDayData();
        popupDayInfo.SetActive(true);
        
        GameManager.Instance.PopupMenuOpened = true;
        Time.timeScale = 0;
        currentDay++;
    }

    private void SetStartDayData()  //Выстраивает внутренность поп-апа для начала дня
    {
        if (currentDay == 1)
        {
            if (desiredWood > 0)
            {
                woodRes.SetActive(true);
                woodText.text = "" + desiredWood;
            }
            
            if (desiredStone > 0)
            {
                stoneRes.SetActive(true);
                stoneText.text = "" + desiredStone;
            }
            
            if (desiredPower > 0)
            {
                powerRes.SetActive(true);
                powerText.text = "" + desiredPower;
            }
            
            if (desiredWater > 0)
            {
                waterRes.SetActive(true);
                waterText.text = "" + desiredWater;
            }
        }
    }
    
    private void SetEndDayData()  //Выстраивает внутренность поп-апа для конца для
    {
        if (currentDay == 1)
        {
            if (GameManager.WoodCount >= desiredWood && GameManager.StoneCount >= desiredStone && GameManager.PowerCount >= desiredPower && GameManager.WaterCount >= desiredWater) 
            {
                GameManager.WoodCount -= desiredWood;
                GameManager.StoneCount -= desiredStone;
                GameManager.PowerCount -= desiredPower;
                GameManager.WaterCount -= desiredWater;

                nameText.text = "Удача!";
                descriptionText.text = "Молодец! Все ресурсы собраны!";
            }
            else
            {
                nameText.text = "Проигрыш!";
                descriptionText.text = "Ты не собрал нужное количество ресурсов. Завтра тебя ждет кара";
            }
        }
    }
    
    private IEnumerator UpdateTimerTextCoroutine()
    {
        while(!_dayDelay.IsReady)
        {
            var remainingTime = _dayDelay.RemainingTime();

            int minutes = Mathf.FloorToInt(remainingTime / SECONDS_IN_MINUTE);
            int seconds = Mathf.FloorToInt(remainingTime % SECONDS_IN_MINUTE);
            
            timeText.text = $"{minutes:00}:{seconds:00}";
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    public void OnStartDayButtonClicked()
    {
        if (currentDay == 1)
        {
            popupDayInfo.SetActive(false);
            _dayDelay.Reset();
            Time.timeScale = 1;
            GameManager.Instance.PopupMenuOpened = false;
            dayText.text = "День: " + currentDay;
            StartCoroutine(UpdateTimerTextCoroutine());
        }
        else
        {
            popupDayInfo.SetActive(false);
            StopCoroutine(UpdateTimerTextCoroutine());
            timeText.text = "";
            dayText.text = "";
            Time.timeScale = 1;
            GameManager.Instance.PopupMenuOpened = false;
            _dayDelay.Stop();
        }
    }
}
