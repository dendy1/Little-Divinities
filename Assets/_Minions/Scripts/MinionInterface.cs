using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MinionBaseStats))]
[RequireComponent(typeof(MinionStats))]
public class MinionInterface : MonoBehaviour
{
    private MinionBaseStats _baseStats;
    private MinionStats _stats;
    
    [Header("UI Settings")]
    public Canvas canvas;
    public Image hpBarImage;
    public Image whiteBarImage;
    public Image badHpImage;
    public Image goodHpImage;
    
    [Range(0.5f, 3f)]
    public float uiAnimationSpeed = 2;
    
    private void Awake()
    {
        _baseStats = GetComponent<MinionBaseStats>();
        _stats = GetComponent<MinionStats>();
    }

    private void Start()
    {
        canvas.enabled = false;
    }
    
    private void FixedUpdate()
    {
        whiteBarImage.fillAmount = Mathf.Lerp(whiteBarImage.fillAmount, hpBarImage.fillAmount, uiAnimationSpeed * Time.deltaTime);
    }

    public void UpdateHealthBar()
    {
        var coef = _stats.currentHp / _baseStats.hp;
        hpBarImage.color = new Color(1 - coef, coef, coef);
        hpBarImage.fillAmount = coef;
        
        badHpImage.gameObject.SetActive(_stats.currentHp < _baseStats.hp * 0.1);
    }

    public void SetActiveGoodHPImage(bool active)
    {
        goodHpImage.gameObject.SetActive(active);
    }
}
