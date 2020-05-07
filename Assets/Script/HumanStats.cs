using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanStats : MonoBehaviour
{
    [Header("Actual Stats")]
    [SerializeField] private int food;
    [SerializeField] private int health;
    [SerializeField] private int economic;
    [SerializeField] private int happiness;
    
    public int Food
    {
        get => food;
        set
        {
            food = value;
            foodBar.fillAmount = food / 10f;
        }
    }

    public int Health
    {
        get => health;
        set
        {
            health = value;
            healthBar.fillAmount = health / 10f;
        }
    }

    public int Economic
    {
        get => economic;
        set
        {
            economic = value;
            economicBar.fillAmount = economic / 10f;
        }
    }

    public int Happiness
    {
        get => happiness;
        set
        {
            happiness = value;
            happinessBar.fillAmount = happiness / 10f;
        }
    }

    [Header("UI Elements")]
    public Image economicBar;
    public Image foodBar;
    public Image happinessBar;
    public Image healthBar;
    
    private void Start()
    {
        Food = Health = Economic = Happiness = 5;
    }
}
