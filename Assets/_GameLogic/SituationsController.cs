using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

[RequireComponent(typeof(HumanStats))]
public class SituationsController : MonoBehaviour
{
    // Situations data for popup
    private readonly List<string> _names = new List<string>(); // Title of Situation
    private readonly List<string> _descriptions = new List<string>(); // Description of Situation
    private readonly List<string> _goods = new List<string>(); // Text for good choice
    private readonly List<string> _bads = new List<string>(); // Text for bad choice
    
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
    private HumanStats _humanStats;
    private Delay _situationDelay;
    private Delay _skipDelay;

    [SerializeField] private float minTime = 30f;
    [SerializeField] private float maxTime = 50f;
    
    private GameManager GameManager => GameManager.Instance;

    private void Awake()
    {
        _humanStats = GetComponent<HumanStats>();
        _situationDelay = new Delay(10f);
        _skipDelay = new Delay(ignoreTime, true);
    }

    private void Start()
    {
        _situationId = 0;

        situationPopup.SetActive(false);
        skipPopup.SetActive(false);

        #region Имя
        _names.Add("Затор на шахте");
        _names.Add("Метеорит");
        _names.Add("Шутка про быкова");
        #endregion

        #region Описание
        _descriptions.Add("Во время строительных работ в колодце, на рабочих обрушилась груда камней.Под завалами оказались десятки человек.");
        _descriptions.Add("Глубокой ночью на город упал метеорит. Все люди в панике убегают от места события.");
        _descriptions.Add("Фермеры устроили забастовку из-за нехватки электричества в поселках.");
        _descriptions.Add("Бобры целый год строили плотину на реке, но из-за сильных штормов плотины не стало.");
        _descriptions.Add("На выпускном у старшеклассников, родители перебрали спиртного и разнесли школу в щепки.");
        _descriptions.Add("Волонтеры открыли новый пункт помощи бездомным прямо в центре города.");
        _descriptions.Add("Почти все население города страдает от алкоголизма");
        _descriptions.Add("Студенты местного университета заняли 1 место в мире на олимпиаде по математике");
        _descriptions.Add("Чиновники города замечены в коррупции немыслимых масштабов");
        _descriptions.Add("Город утопает в грязи, смоге и мусоре");
        #endregion

        #region Хороший выбор
        _goods.Add("1. Отправить продовольствие спасателям.");
        _goods.Add("1. Поддержать государство припасами");
        _goods.Add("1. Выделить фермерам электричества из собственных запасов");
        _goods.Add("1. Помочь бобрам построить плотину");
        _goods.Add("1. Поделиться ресурсами на восстановление школы");
        _goods.Add("1. Организовать бесплатную распечатку книг для них");
        _goods.Add("1. Открыть реабилитационные центры");
        _goods.Add("1. Организовать компьютерные центры для стимуляции образования");
        _goods.Add("1. Построить изоляторы для государственных преступников");
        _goods.Add("1. Создать систему очистных сооружений");
        #endregion

        #region Плохой выбор
        _bads.Add("2. Камень я дам.Помощь я не ДАМ! Отправить на стройку больше камней.");
        _bads.Add("2. Запустить на город второй метеорит");
        _bads.Add("2. Залить поселок водой, чтобы электричество вовсе пропало");
        _bads.Add("2. Переполнить реку, чтобы бобров смыло вместе с плотиной.");
        _bads.Add("2. Пригласить лучшего диджея, чтобы вечеринка не кончалась");
        _bads.Add("2. Уничтожить пункт помощи, очистив планету от генетического мусора");
        _bads.Add("2. Организовать массовую перегонку дерева в спирт");
        _bads.Add("2. Отчислить их из университета, чтобы остальные не чувствовали себя глупыми");
        _bads.Add("2. Выделить чиновникам дополнительные субсидии на постройку особняков");
        _bads.Add("2. В довесок запылить город отходами с каменоломни");
        #endregion
    }

    private void Update()
    {
        if (_situationId >= _names.Count)
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
        if (_situationId == 0)
        {
            if (GameManager.WoodCount >= 50)
            {
                GameManager.WoodCount -= 50;
            }
        }
        
        if (_situationId == 1)
        {
            if (GameManager.PowerCount >= 20 && GameManager.WoodCount >= 20)
            {
                GameManager.PowerCount -= 20;
                GameManager.WoodCount -= 20;
            }
        }
        
        if (_situationId == 2)
        {
            if (GameManager.PowerCount >= 30)
            {
                GameManager.PowerCount -= 30;
            }
        }
        
        if (_situationId == 3)
        {
            if (GameManager.WoodCount >= 50)
            {
                GameManager.WoodCount -= 50;
            }
        }
        
        if (_situationId == 4)
        {
            if (GameManager.StoneCount >= 20 && GameManager.WoodCount >= 20)
            {
                GameManager.StoneCount -= 20;
                GameManager.WoodCount -= 20;
            }
        }
        
        if (_situationId == 5)
        {
            if (GameManager.WoodCount >= 20)
            {
                GameManager.WoodCount -= 20;
            }
        }
    }

    private void BadChoicePrice()
    {
        if (_situationId == 0)
        {
            if (GameManager.StoneCount >= 50)
            {
                GameManager.StoneCount -= 50;
            }
        }
        
        if (_situationId == 1)
        {
            if (GameManager.PowerCount >= 30 && GameManager.StoneCount >= 20)
            {
                GameManager.PowerCount -= 30;
                GameManager.StoneCount -= 20;
            }
        }
        
        if (_situationId == 2)
        {
            if (GameManager.WaterCount >= 40)
            {
                GameManager.WaterCount -= 40;
            }
        }
        
        if (_situationId == 3)
        {
            if (GameManager.WaterCount >= 50)
            {
                GameManager.WaterCount -= 50;
            }
        }
        
        if (_situationId == 4)
        {
            if (GameManager.WaterCount >= 10 && GameManager.PowerCount >= 30)
            {
                GameManager.WaterCount -= 10;
                GameManager.PowerCount -= 30;
            }
        }
        
        if (_situationId == 5)
        {
            if (GameManager.PowerCount >= 40)
            {
                GameManager.PowerCount -= 40;
            }
        }
    }

    private void GoodChoiceResult()
    {
        if (_situationId == 0)
        {
            _humanStats.Health++;
            _humanStats.Happiness++;
        }
        
        if (_situationId == 1)
        {
            _humanStats.Health++;
            _humanStats.Happiness++;
        }
        
        if (_situationId == 2)
        {
            _humanStats.Food++;
        }
        
        if (_situationId == 3)
        {
            _humanStats.Health++;
            _humanStats.Food++;
        }
        
        if (_situationId == 4)
        {
            _humanStats.Economic++;
            _humanStats.Happiness++;
        }
        
        if (_situationId == 5)
        {
            _humanStats.Economic++;
            _humanStats.Happiness++;
        }

        _situationId++;
        _situationDelay.WaitTime = Random.Range(minTime, maxTime);
        _situationDelay.Reset();
        
        Time.timeScale = 1;
        situationPopup.SetActive(false);
    }

    private void BadChoiceResult()
    {
        if (_situationId == 0)
        {
            _humanStats.Happiness--;
            _humanStats.Health -= 2;
        }
        
        if (_situationId == 1)
        {
            _humanStats.Health--;
            _humanStats.Happiness--;
            _humanStats.Economic--;
        }
        
        if (_situationId == 2)
        {
            _humanStats.Food--;
            _humanStats.Health--;
        }
        
        if (_situationId == 3)
        {
            _humanStats.Health--;
            _humanStats.Food--;
        }
        
        if (_situationId == 4)
        {
            _humanStats.Economic--;
            _humanStats.Health--;
        }
        
        if (_situationId == 5)
        {
            _humanStats.Food++;
            _humanStats.Health++;
        }
        
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
        nameText.text = _names[_situationId];
        descriptionText.text = _descriptions[_situationId];
        goodText.text = _goods[_situationId];
        badText.text = _bads[_situationId];
        
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
        if (_situationId == 0)
        {
            woodRes.SetActive(true);
            woodText.text = "-50";

            healthRes.SetActive(true);            
            healthText.text = "++";
            happinessRes.SetActive(true);
            happinessText.text = "++";
        }
        
        if (_situationId == 1)
        {
            woodRes.SetActive(true);
            woodText.text = "-20";
            powerRes.SetActive(true);
            powerText.text = "-20";

            healthRes.SetActive(true);
            healthText.text = "++";
            happinessRes.SetActive(true);
            happinessText.text = "++";
        }
        
        if (_situationId == 2)
        {
            powerRes.SetActive(true);
            powerText.text = "-30";

            foodRes.SetActive(true);
            foodText.text = "++";
        }
        
        if (_situationId == 3)
        {
            woodRes.SetActive(true);
            woodText.text = "-50";

            foodRes.SetActive(true);
            foodText.text = "++";
            healthRes.SetActive(true);
            healthText.text = "++";
        }
        
        if (_situationId == 4)
        {
            woodRes.SetActive(true);
            woodText.text = "-20";
            stoneRes.SetActive(true);
            stoneText.text = "-20";
            
            economyRes.SetActive(true);
            economyText.text = "++";
            happinessRes.SetActive(true);
            happinessText.text = "++";
        }
        
        if (_situationId == 5)
        {
            woodRes.SetActive(true);
            woodText.text = "-50";
            
            economyRes.SetActive(true);
            economyText.text = "++";
            happinessRes.SetActive(true);
            happinessText.text = "++";
        }
    }

    public void OnEnterBadChoiceButton() // Если игрок навёл на кнопку с плохим выбором
    {
        if (_situationId == 0)
        {
            stoneRes.SetActive(true);
            stoneText.text = "-50";

            happinessRes.SetActive(true);
            happinessText.text = "--";
            healthRes.SetActive(true);
            healthText.text = "--";
        }

        if (_situationId == 1)
        {
            stoneRes.SetActive(true);
            stoneText.text = "-20";
            powerRes.SetActive(true);
            powerText.text = "-30";

            happinessRes.SetActive(true);
            happinessText.text = "--";
            healthRes.SetActive(true);
            healthText.text = "--";
            economyRes.SetActive(true);
            economyText.text = "--";
        }

        if (_situationId == 2)
        {
            waterRes.SetActive(true);
            waterText.text = "-40";

            foodRes.SetActive(true);
            foodText.text = "--";
            healthRes.SetActive(true);
            healthText.text = "--";
        }

        if (_situationId == 3)
        {
            waterRes.SetActive(true);
            waterText.text = "-50";

            foodRes.SetActive(true);
            foodText.text = "--";
            healthRes.SetActive(true);
            healthText.text = "--";
        }

        if (_situationId == 4)
        {
            waterRes.SetActive(true);
            waterText.text = "-10";
            powerRes.SetActive(true);
            powerText.text = "-30";

            economyRes.SetActive(true);
            economyText.text = "--";
            healthRes.SetActive(true);
            healthText.text = "--";
        }
        
        if (_situationId == 5)
        {
            powerRes.SetActive(true);
            powerText.text = "-40";

            foodRes.SetActive(true);
            foodText.text = "--";
            healthRes.SetActive(true);
            healthText.text = "--";
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
