using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MinionDescriptor : MonoBehaviour
{
    
    
    public GameObject icon_Hp_Good, icon_Hp_Bad;
    
    public float MaxHp { get; private set; }
    public float Power { get; private set; }
    public float DieRate { get; private set; }
    
    public float currentMaxHP;
    public float currentHealth;
    public float currentPower;
    public float currentDieRate = 1f;
    public Type type;
    
    private bool _working, _restoring, _merging, _moving;
    
    public bool Working
    {
        get => _working;
        set
        {
            _restoring = false;
            _merging = false;
            _moving = false;
            _working = true;
        }
    }

    public bool Restornig
    {
        get => _restoring;
        set
        {
            _restoring = true;
            _merging = false;
            _working = false;
            _moving = false;
        }
    }
    
    public bool Merging
    {
        get => _merging;
        set
        {
            _restoring = false;
            _merging = true;
            _working = false;
            _moving = false;
        }
    }
    
    public bool Moving
    {
        get => _moving;
        set
        {
            _restoring = false;
            _merging = false;
            _working = false;
            _moving = true;
        }
    }

    private EnemyHealthController _HBcontroller;
    
    public enum Type
    {
        Forester,
        Stoner,
        Waterman,
        Energyman,
        Crystalman
    }

    private void Start()
    {
        _HBcontroller = GetComponent<EnemyHealthController>();
    }

    public IEnumerator WorkCycle()
    {
        while (!Moving && Working && currentHealth > 0)
        {
            yield return new WaitForSeconds(1f);
            currentHealth -= currentDieRate;
            //GameManager.Instance.Harvest(type, currentPower);
            transform.parent.GetComponent<IslandController>().dobicha = true;
            icon_Hp_Good.SetActive(false);
            if (currentHealth < currentMaxHP)
            {
                _HBcontroller.Canvas.enabled = true;
                _HBcontroller.ReceiveDamage(1);
                if (currentHealth < 10)
                {
                    icon_Hp_Bad.SetActive(true);
                }
                if (currentHealth >= 10)
                {
                    icon_Hp_Bad.SetActive(false);
                }
            }
        }
        
        if (currentHealth < 1)
        {
            Die();
        }
    }

    public IEnumerator RestoreHP()
    {
        while (currentHealth < currentMaxHP && !Moving)
        {
            yield return new WaitForSeconds(1f);
            currentHealth += 2;
            _HBcontroller.Heal(2);
            icon_Hp_Good.SetActive(false);
            
            if (currentHealth >= currentMaxHP)
            {
                //_HBcontroller.Canvas.enabled = false;
            }
        }

        if (currentHealth >= currentMaxHP)
        {
            icon_Hp_Good.SetActive(true);
            currentHealth = currentMaxHP;
        }
    }

    public void WhenPlacedOnRestoration()
    {
        transform.parent.GetComponent<IslandController>().dobicha = false;
        Debug.Log("RESTORATION");
        if (Restornig)
            return;

        Restornig = true;
        
        icon_Hp_Bad.SetActive(false);
        StopCoroutine(WorkCycle());
        StartCoroutine(RestoreHP());
        
    }
    
    public void WhenPlacedOnWorking()
    {
        Debug.Log("WORKING");
        if (Working)
            return;

        Working = true;
        StopCoroutine(RestoreHP());
        StartCoroutine(WorkCycle());
        
    }
    
    public void WhenPlacedOnMerging()
    {
        transform.parent.GetComponent<IslandController>().dobicha = false;
        Debug.Log("MERGING");
        if (Merging)
            return;

        Merging = true;
        
        
        StopCoroutine(RestoreHP());
        StopCoroutine(WorkCycle());
    }

    public void WhenMoving()
    {
        transform.parent.GetComponent<IslandController>().dobicha = false;
        Debug.Log("MOVING");
        if (Moving)
            return;

        Moving = true;
        
    }
    
    public void Die()
    {
        GameObject part = Instantiate(GameObject.Find("GameManager").GetComponent<GameManager>().deathParticles, transform.position,
            transform.rotation) as GameObject;
        Destroy(part, 2);
        dead?.Invoke();
        StopAllCoroutines();
        Destroy(gameObject);
        transform.parent.GetComponent<IslandController>().dobicha = false;
    }

    public void SetParameters(Type type, float maxHP, float power)
    {
        this.type = type;
        currentHealth = maxHP;
        currentPower = power;
        currentMaxHP = maxHP;
        
        MaxHp = maxHP;
        Power = power;
        DieRate = currentDieRate;
    }

    public void ResetParameters()
    {
        currentMaxHP = MaxHp;
        currentPower = Power;
        currentDieRate = DieRate;
    }

    public UnityEvent dead = new UnityEvent();
}
