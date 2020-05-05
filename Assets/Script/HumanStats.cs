using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanStats : MonoBehaviour
{
    public int eat;
    public int health;
    public int economic;
    public int happy;

    public Image eat_bar;
    public Image health_bar;
    public Image economic_bar;
    public Image happy_bar;


    private void Start()
    {
        eat = health = economic = happy = 5;
    }

    private void Update()
    {
        eat_bar.fillAmount = eat / 10f;
        health_bar.fillAmount = health / 10f;
        economic_bar.fillAmount = economic / 10f;
        happy_bar.fillAmount = happy / 10f;
    }
}
