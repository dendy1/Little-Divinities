using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  TMPro;

public class DayController : MonoBehaviour
{
    public int curr_day;

    public GameObject pop_upDayStart, pop_upDayFinish;
    
    public GameObject woodRes, stoneRes, waterRes, energyRes;
    public TMP_Text woodText, stoneText, waterText, energyText, descriptionText;
    public float wood, stone, water, energy;

    private bool timeStart;

    public TMP_Text dayText, timeText;
    public int min, sec;


    private void Start()
    {
        min = 10;
        sec = 0;
        
        curr_day = 1;
        pop_upDayStart.SetActive(true);
        pop_upDayFinish.SetActive(false);

        DayFromPopUp();

        GameManager.Instance.PopupMenuOpened = true;
    }

    public void EndDayPopUp()
    {
        Time.timeScale = 0;
        pop_upDayFinish.SetActive(true);
        
        if (curr_day == 1)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().WoodCount >= wood &&
                GameObject.Find("GameManager").GetComponent<GameManager>().StoneCount >= stone) 
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().WoodCount -= wood;
                GameObject.Find("GameManager").GetComponent<GameManager>().StoneCount -= stone;
                descriptionText.text = "Молодец! Все ресурсы собраны!";
            } else descriptionText.text = "Ты не собрал нужное количество ресурсов. Завтра тебя ждет кара";
        }
    }

    public void ButNextDay()
    {
        
    }

    public void ButStartDay()
    {
        pop_upDayStart.SetActive(false);
        Time.timeScale = 1;
        timeStart = true;
        StartCoroutine(TimeGL());
        GameManager.Instance.PopupMenuOpened = false;
        StartCoroutine(TimerDay());
    }

    public void DayFromPopUp()  //Выстраивает внутренность поп-апа
    {
        if (curr_day == 1)
        {
            woodRes.SetActive(true);
            stoneRes.SetActive(true);
            waterRes.SetActive(false);
            energyRes.SetActive(false);

            wood = 50;
            stone = 50;

            woodText.text = "" + wood;
            stoneText.text = "" + stone;

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
        dayText.text = "День " + curr_day;
        
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
