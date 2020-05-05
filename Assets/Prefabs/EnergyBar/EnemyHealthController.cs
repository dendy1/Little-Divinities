using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    [Header("UI Settings")]

    public Canvas Canvas;

    public Image MainEB;

    public Image WhiteEBPart;

    [Header("EnergyBar Settings")]
    public int StartHealth = 60;

    [Range(0.5f, 3f)]
    public float UIAnimSpeed = 2;

    private int _currentEnergy;  

    void Start()
    {
        _currentEnergy = StartHealth;
        Canvas.enabled = false;
    }

    private bool _isSwapping = false;

    public void ReceiveDamage(int damage)
    {
        _currentEnergy -= damage;
        var coeff = (float)_currentEnergy / StartHealth;
        MainEB.color = new Color(1 - coeff, coeff, coeff);
        MainEB.fillAmount = (float)_currentEnergy / (float)StartHealth;
    }
    
    public void Heal(int healvalue)
    {
        _currentEnergy += healvalue;
        var coeff = (float)_currentEnergy / StartHealth;
        MainEB.color = new Color(1 - coeff, coeff, coeff);
        MainEB.fillAmount = (float)_currentEnergy / (float)StartHealth;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Для расстояния
        //if (Vector3.Distance(Canvas.transform.position, Camera.main.transform.position) < 30) Canvas.enabled = true;
        WhiteEBPart.fillAmount = Mathf.Lerp(WhiteEBPart.fillAmount, MainEB.fillAmount, UIAnimSpeed * Time.deltaTime);
    }

}
