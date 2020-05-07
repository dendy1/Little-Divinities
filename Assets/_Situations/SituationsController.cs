using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class SituationsController : MonoBehaviour
{
    List<string> name = new List<string>();
    List<string> description = new List<string>();
    List<string> good_text = new List<string>();
    List<string> bad_text = new List<string>();

    public TMP_Text name_t, description_t, good_t, bad_t;

    private int id;
    private float timerSituation;

    public GameObject situationWindow, popup_nothfing, popup_nothfing2;

    public GameObject messageObj;  //Сообщение

    public GameObject woodRes, stoneRes, waterRes, energyRes;
    public TMP_Text woodText, stoneText, waterText, energyText;

    private GameManager gm;
    private HumanStats human;

    private void Awake()
    {
        messageObj.GetComponent<Animator>().SetBool("active", false);
    }

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        human = GameObject.Find("GameManager").GetComponent<HumanStats>();
        
        id = 0;
        situationWindow.SetActive(false);
        popup_nothfing.SetActive(false);

        timerSituation = Random.Range(80, 140);
        StartCoroutine(TimerSituation());
        
        #region Имя
        name.Add("Затор на шахте");
        name.Add("Метеорит");
        name.Add("Шутка про быкова");
        #endregion

        #region Описание
        description.Add("Во время строительных работ в колодце, на рабочих обрушилась груда камней.Под завалами оказались десятки человек.");
        description.Add("Глубокой ночью на город упал метеорит. Все люди в панике убегают от места события.");
        description.Add("Фермеры устроили забастовку из-за нехватки электричества в поселках.");
        description.Add("Бобры целый год строили плотину на реке, но из-за сильных штормов плотины не стало.");
        description.Add("На выпускном у старшеклассников, родители перебрали спиртного и разнесли школу в щепки.");
        description.Add("Волонтеры открыли новый пункт помощи бездомным прямо в центре города.");
        description.Add("Почти все население города страдает от алкоголизма");
        description.Add("Студенты местного университета заняли 1 место в мире на олимпиаде по математике");
        description.Add("Чиновники города замечены в коррупции немыслимых масштабов");
        description.Add("Город утопает в грязи, смоге и мусоре");
        #endregion

        #region Хороший выбор
        good_text.Add("1. Отправить продовольствие спасателям.");
        good_text.Add("1. Поддержать государство припасами");
        good_text.Add("1. Выделить фермерам электричества из собственных запасов");
        good_text.Add("1. Помочь бобрам построить плотину");
        good_text.Add("1. Поделиться ресурсами на восстановление школы");
        good_text.Add("1. Организовать бесплатную распечатку книг для них");
        good_text.Add("1. Открыть реабилитационные центры");
        good_text.Add("1. Организовать компьютерные центры для стимуляции образования");
        good_text.Add("1. Построить изоляторы для государственных преступников");
        good_text.Add("1. Создать систему очистных сооружений");
        #endregion

        #region Плохой выбор
        bad_text.Add("2. Камень я дам.Помощь я не ДАМ! Отправить на стройку больше камней.");
        bad_text.Add("2. Запустить на город второй метеорит");
        bad_text.Add("2. Залить поселок водой, чтобы электричество вовсе пропало");
        bad_text.Add("2. Переполнить реку, чтобы бобров смыло вместе с плотиной.");
        bad_text.Add("2. Пригласить лучшего диджея, чтобы вечеринка не кончалась");
        bad_text.Add("2. Уничтожить пункт помощи, очистив планету от генетического мусора");
        bad_text.Add("2. Организовать массовую перегонку дерева в спирт");
        bad_text.Add("2. Отчислить их из университета, чтобы остальные не чувствовали себя глупыми");
        bad_text.Add("2. Выделить чиновникам дополнительные субсидии на постройку особняков");
        bad_text.Add("2. В довесок запылить город отходами с каменоломни");
        #endregion
    }

    public void StartSituation()
    {
        StopAllCoroutines();
        messageObj.GetComponent<Animator>().SetBool("active", false);
        StartCoroutine(StopTime());
        situationWindow.SetActive(true);

        name_t.text = name[id];
        description_t.text = description[id];
        good_t.text = good_text[id];
        bad_t.text = bad_text[id];
    }

    public void Good_Situation_Price()
    {
        if (id == 0)
        {
            if (gm.WoodCount >= 50)
            {
                gm.WoodCount -= 50;
            }
        }
        
        if (id == 1)
        {
            if (gm.PowerCount >= 20 && gm.WoodCount >= 20)
            {
                gm.PowerCount -= 20;
                gm.WoodCount -= 20;
            }
        }
        
        if (id == 2)
        {
            if (gm.PowerCount >= 30)
            {
                gm.PowerCount -= 30;
            }
        }
        
        if (id == 3)
        {
            if (gm.WoodCount >= 50)
            {
                gm.WoodCount -= 50;
            }
        }
        
        if (id == 4)
        {
            if (gm.StoneCount >= 20 && gm.WoodCount >= 20)
            {
                gm.StoneCount -= 20;
                gm.WoodCount -= 20;
            }
        }
        
        if (id == 5)
        {
            if (gm.WoodCount >= 20)
            {
                gm.WoodCount -= 20;
            }
        }
    }

    public void Bad_Situation_Price()
    {
        if (id == 0)
        {
            if (gm.StoneCount >= 50)
            {
                gm.StoneCount -= 50;
            }
        }
        
        if (id == 1)
        {
            if (gm.PowerCount >= 30 && gm.StoneCount >= 20)
            {
                gm.PowerCount -= 30;
                gm.StoneCount -= 20;
            }
        }
        
        if (id == 2)
        {
            if (gm.WaterCount >= 40)
            {
                gm.WaterCount -= 40;
            }
        }
        
        if (id == 3)
        {
            if (gm.WaterCount >= 50)
            {
                gm.WaterCount -= 50;
            }
        }
        
        if (id == 4)
        {
            if (gm.WaterCount >= 10 && gm.PowerCount >= 30)
            {
                gm.WaterCount -= 10;
                gm.PowerCount -= 30;
            }
        }
        
        if (id == 5)
        {
            if (gm.PowerCount >= 40)
            {
                gm.PowerCount -= 40;
            }
        }
    }

    public void Good_Situation_Result()
    {
        if (id == 0)
        {
            human.health++;
            human.happy++;
        }
        
        if (id == 1)
        {
            human.health++;
            human.happy++;
        }
        
        if (id == 2)
        {
            human.eat++;
        }
        
        if (id == 3)
        {
            human.health++;
            human.eat++;
        }
        
        if (id == 4)
        {
            human.economic++;
            human.happy++;
        }
        
        if (id == 5)
        {
            human.economic++;
            human.happy++;
        }

        Time.timeScale = 1;
        id++;
        situationWindow.SetActive(false);

        timerSituation = Random.Range(80, 140);
        StartCoroutine(TimerSituation());
    }

    public void Bad_Situation_Result()
    {
        if (id == 0)
        {
            human.happy--;
            human.health -= 2;
        }
        
        if (id == 1)
        {
            human.health--;
            human.happy--;
            human.economic--;
        }
        
        if (id == 2)
        {
            human.eat--;
            human.health--;
        }
        
        if (id == 3)
        {
            human.health--;
            human.eat--;
        }
        
        if (id == 4)
        {
            human.economic--;
            human.health--;
        }
        
        if (id == 5)
        {
            human.eat++;
            human.health++;
        }
        
        Time.timeScale = 1;
        id++;
        situationWindow.SetActive(false);

        timerSituation = Random.Range(80, 140);
        StartCoroutine(TimerSituation());
    }
    
    public void NotChoseSituation()  //если игрок не нажал на ситуацию
    {
        messageObj.GetComponent<Animator>().SetBool("active", false);
        popup_nothfing.SetActive(true);
        
        Time.timeScale = 1;
        situationWindow.SetActive(false);
    }
    
    public void NothingSituation()  //если игрок выбрал ничего не делать
    {
        popup_nothfing2.SetActive(true);
        
        Time.timeScale = 1;
        id++;
        situationWindow.SetActive(false);

        timerSituation = Random.Range(80, 140);
        StartCoroutine(TimerSituation());
    }

    public void Good_OnEnter()
    {
        if (id == 0)
        {
            woodRes.SetActive(true);
            woodText.text = "-50";
        }
        
        if (id == 1)
        {
            woodRes.SetActive(true);
            energyRes.SetActive(true);
            woodText.text = "-20";
            energyText.text = "-20";
        }
        
        if (id == 2)
        {
            energyRes.SetActive(true);
            energyText.text = "-30";
        }
        
        if (id == 3)
        {
            woodRes.SetActive(true);
            woodText.text = "-50";
        }
        
        if (id == 4)
        {
            woodRes.SetActive(true);
            woodText.text = "-20";
            stoneRes.SetActive(true);
            stoneText.text = "-20";
        }
        
        if (id == 5)
        {
            woodRes.SetActive(true);
            woodText.text = "-50";
        }
    }
    
    public void Bad_OnEnter()
    {
        if (id == 0)
        {
            stoneRes.SetActive(true);
            stoneText.text = "-50";
        }
        
        if (id == 1)
        {
            stoneRes.SetActive(true);
            stoneText.text = "-20";
            energyRes.SetActive(true);
            energyText.text = "-30";
        }
        
        if (id == 2)
        {
            waterRes.SetActive(true);
            waterText.text = "-40";
        }
        
        if (id == 3)
        {
            waterRes.SetActive(true);
            waterText.text = "-50";
        }
        
        if (id == 4)
        {
            waterRes.SetActive(true);
            waterText.text = "-10";
            energyRes.SetActive(true);
            energyText.text = "-30";
        }
        
        if (id == 5)
        {
            energyRes.SetActive(true);
            energyText.text = "-40";
        }
    }

    public void On_Exit_Button_Change()  //Если игрок убрал курсор с любой кнопки выбора
    {
        woodRes.SetActive(false);
        stoneRes.SetActive(false);
        waterRes.SetActive(false);
        energyRes.SetActive(false);
    }

    IEnumerator TimerSituation()
    {
        yield return new WaitForSeconds(timerSituation);
        messageObj.GetComponent<Animator>().SetBool("active", true);
        StartCoroutine(TimeToShtraf());
    }
    
    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(1.1f);
        Time.timeScale = 0;
    }
    
    IEnumerator TimeToShtraf()
    {
        yield return new WaitForSeconds(10f);
        NotChoseSituation();
    }

    public void Shtraf()
    {
        gm.WoodCount -= 10;
        gm.StoneCount -= 10;
        gm.PowerCount -= 10;
        gm.WaterCount -= 10;
        
        timerSituation = Random.Range(5, 9);
        StartCoroutine(TimerSituation());
        popup_nothfing.SetActive(false);
        popup_nothfing2.SetActive(false);
    }
}
