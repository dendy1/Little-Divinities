using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MinionBaseStats))]
[RequireComponent(typeof(MinionInterface))]
public class MinionStats : MonoBehaviour
{
    private StateMachine _stateMachine;
    private MinionBaseStats _baseStats;
    private MinionInterface _interface;

    public IdleState idleState;
    public HarvestState harvestState;
    public HealState healState;
    public DieState dieState;
        
    public float currentHp;
    public float currentHarvestRate;
    public float currentFatigueRate;

    public MinionBaseStats BaseStats => _baseStats;

    public UnityEvent dead = new UnityEvent();
    
    private void Awake()
    {
        _stateMachine = new StateMachine();
        _baseStats = GetComponent<MinionBaseStats>();
        _interface = GetComponent<MinionInterface>();
        
        idleState = new IdleState(this, _baseStats, _interface, _stateMachine);
        harvestState = new HarvestState(this, _baseStats, _interface, _stateMachine);
        healState = new HealState(this, _baseStats, _interface, _stateMachine);
        dieState = new DieState(this, _baseStats, _interface, _stateMachine);
    }

    private void Start()
    {
        currentHp = _baseStats.hp;
        currentHarvestRate = _baseStats.harvestRate;
        currentFatigueRate = _baseStats.fatigueRate;
        
        dead.AddListener(OnDead);
        
        _stateMachine.ChangeState(idleState);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void OnDead()
    {
        // GameObject particles = Instantiate(GameManager.Instance.deathParticles, transform.position, transform.rotation);
        // Destroy(particles, 2);
        // dead?.Invoke();
        // StopAllCoroutines();
        Destroy(gameObject);
    }
}
