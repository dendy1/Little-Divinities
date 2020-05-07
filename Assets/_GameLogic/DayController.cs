using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  TMPro;

public class DayController : MonoBehaviour
{
    public int currentDay;

    [Header("GameLogic PopUp GameObjects")] 
    [SerializeField] private GameObject popupDayStart;
    [SerializeField] private GameObject popupDayFinish;

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

    [Header("Desired Resources for Day")] 
    [SerializeField] private float desiredWood;
    [SerializeField] private float desiredStone;
    [SerializeField] private float desiredPower;
    [SerializeField] private float desiredWater;

    private bool timeStart;

    [Header("Day/Time TextField for UI")] 
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private TMP_Text timeText;
    
    
    public int min, sec;

    private GameManager GameManager => GameManager.Instance;

    private void Start()
    {
        min = 10;
        sec = 0;
        
        currentDay = 1;
        popupDayStart.SetActive(true);
        popupDayFinish.SetActive(false);

        DayFromPopUp();

        GameManager.Instance.PopupMenuOpened = true;
    }

    public void EndDayPopUp()
    {
        Time.timeScale = 0;
        popupDayFinish.SetActive(true);
        
        if (currentDay == 1)
        {
            if (GameManager.WoodCount >= desiredWood && GameManager.StoneCount >= desiredStone) 
            {
                GameManager.WoodCount -= desiredWood;
                GameManager.StoneCount -= desiredStone;
                
                descriptionText.text = "Молодец! Все ресурсы собраны!";
            }
            else
            {
                descriptionText.text = "Ты не собрал нужное количество ресурсов. Завтра тебя ждет кара";
            }
        }
    }

    public void ButNextDay()
    {
        // TODO: СДЕЛАТЬ ПЕРЕХОД К НОВОМУ ДНЮ
    }

    public void ButStartDay()
    {
        popupDayStart.SetActive(false);
        Time.timeScale = 1;
        timeStart = true;
        StartCoroutine(TimeGL());
        GameManager.Instance.PopupMenuOpened = false;
        StartCoroutine(TimerDay());
    }

    public void DayFromPopUp()  //Выстраивает внутренность поп-апа
    {
        if (currentDay == 1)
        {
            woodRes.SetActive(true);
            stoneRes.SetActive(true);
            waterRes.SetActive(false);
            powerRes.SetActive(false);

            desiredWood = 50;
            desiredStone = 50;

            woodText.text = "" + desiredWood;
            stoneText.text = "" + desiredStone;

            Time.timeScale = 0;
        }
    }

    IEnumerator TimerDay()
    {
        timeStart = true;
        yield return new WaitForSeconds(720);
        timeStart = false;
        StartCoroutine(TimeGL());
        EndDayPopUp();
    }

    private void Update()
    {
        dayText.text = "День " + currentDay;
        
        if (timeStart)
        {
            if (sec < 10)
            {
                timeText.text = min + ":0" + sec;
            }
            
            if (sec >= 10)
            {
                timeText.text = min + ":" + sec;
            }
        }
    }

    IEnumerator TimeGL()
    {
        yield return new WaitForSeconds(1);
        sec++;
        if (sec == 60)
        {
            min++;
            sec = 0;
        }

        StartCoroutine(TimeGL());
    }
}
