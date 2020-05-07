using UnityEngine;
using UnityEngine.EventSystems;

public class MinionShopController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Logic Components")]
    [SerializeField] private GameObject minionPrefab;
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
        var minionStats = minionPrefab.GetComponent<MinionBaseStats>();

        _shopButtonController.priceText.text = minionStats.cost.ToString();
        _shopButtonController.iconImage.sprite = minionStats.iconSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShopManager.Instance.BuyMinion(minionPrefab, islandController);
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
