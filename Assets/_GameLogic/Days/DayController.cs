using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayController : MonoBehaviour
{
    public int currentDay;

    [Header("Days List")] 
    [SerializeField] private List<DayScriptable> days;

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
    private int _currentDayIndex = 0;
    private DayScriptable CurrentDay => days[_currentDayIndex];

    private void Awake()
    {
        _dayDelay = new Delay(CurrentDay.minutes * SECONDS_IN_MINUTE + CurrentDay.seconds, true);
    }

    private void Start()
    {
        timeText.text = "";
        dayText.text = "";

        if (_currentDayIndex < days.Count)
        {
            SetStartDayData();
            popupDayInfo.SetActive(true);

            InterfaceManager.Instance.PopupMenuOpened = true;
            Time.timeScale = 0;
        }
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
        
        InterfaceManager.Instance.PopupMenuOpened = true;
        Time.timeScale = 0;
        _currentDayIndex++;
    }

    private void SetStartDayData()  //Выстраивает внутренность поп-апа для начала дня
    {
        if (CurrentDay.desiredWood > 0)
        {
            woodRes.SetActive(true);
            woodText.text = "" + CurrentDay.desiredWood;
        }
            
        if (CurrentDay.desiredStone > 0)
        {
            stoneRes.SetActive(true);
            stoneText.text = "" + CurrentDay.desiredStone;
        }
            
        if (CurrentDay.desiredPower > 0)
        {
            powerRes.SetActive(true);
            powerText.text = "" + CurrentDay.desiredPower;
        }
            
        if (CurrentDay.desiredWater > 0)
        {
            waterRes.SetActive(true);
            waterText.text = "" + CurrentDay.desiredWater;
        }
    }
    
    private void SetEndDayData()  //Выстраивает внутренность поп-апа для конца для
    {
        if (GameManager.WoodCount >= CurrentDay.desiredWood && 
            GameManager.StoneCount >= CurrentDay.desiredStone && 
            GameManager.PowerCount >= CurrentDay.desiredPower && 
            GameManager.WaterCount >= CurrentDay.desiredWater) 
        {
            GameManager.WoodCount -= CurrentDay.desiredWood;
            GameManager.StoneCount -= CurrentDay.desiredStone;
            GameManager.PowerCount -= CurrentDay.desiredPower;
            GameManager.WaterCount -= CurrentDay.desiredWater;

            nameText.text = "Удача!";
            descriptionText.text = "Молодец! Все ресурсы собраны!";
        }
        else
        {
            nameText.text = "Проигрыш!";
            descriptionText.text = "Ты не собрал нужное количество ресурсов. Завтра тебя ждет кара";
        }
    }
    
    private IEnumerator UpdateTimerTextCoroutine()
    {
        while (!_dayDelay.IsReady)
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
        popupDayInfo.SetActive(false);
        Time.timeScale = 1;
        InterfaceManager.Instance.PopupMenuOpened = false;
        
        if (_currentDayIndex < days.Count)
        {
            _dayDelay.Reset();
            dayText.text = "День: " + (_currentDayIndex + 1);
            StartCoroutine(UpdateTimerTextCoroutine());
        }
        else
        {
            _dayDelay.Stop();
            StopCoroutine(UpdateTimerTextCoroutine());
            timeText.text = "";
            dayText.text = "Free game";
        }
    }
}
