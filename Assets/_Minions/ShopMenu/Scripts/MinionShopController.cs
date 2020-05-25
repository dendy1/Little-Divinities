using UnityEngine;
using UnityEngine.EventSystems;

public class MinionShopController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Logic Components")]
    [SerializeField] private IslandController islandController;

    [Header("UI Components")]
    [SerializeField] private Sprite backgroundSprite;
    [SerializeField] private Sprite activeBackgroundSprite;

    private ShopButtonController _shopButtonController;
    
    private void Awake()
    {
        _shopButtonController = GetComponent<ShopButtonController>();
    }

    private void Start()
    {
        var minionStats = islandController.Spawner.minionPrefab.GetComponent<MinionStats>().BaseStats;

        _shopButtonController.priceText.text = minionStats.cost.ToString();
        _shopButtonController.iconImage.sprite = minionStats.iconSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ShopManager.Instance.BuyMinion(islandController.Spawner.minionPrefab, islandController))
        {
            var minion = islandController.Spawner.SpawnMinion();
            islandController.AddMinion(minion.GetComponent<MinionStats>());
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _shopButtonController.backgroundImage.sprite = activeBackgroundSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _shopButtonController.backgroundImage.sprite = backgroundSprite;
    }
}
