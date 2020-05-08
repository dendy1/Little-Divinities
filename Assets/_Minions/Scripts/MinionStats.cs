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
    public VacationState vacationState;
    public DieState dieState;
    public MergeState mergeState;
        
    public float currentHp;
    public float currentHarvestRate;
    public float currentFatigueRate;

    public StateMachine StateMachine => _stateMachine;
    public MinionBaseStats BaseStats => _baseStats;
    
    public IslandController MainIslandController;
    public IslandController CurrentIslandController;

    public UnityEvent dead = new UnityEvent();
    
    private void Awake()
    {
        _stateMachine = new StateMachine();
        _baseStats = GetComponent<MinionBaseStats>();
        _interface = GetComponent<MinionInterface>();
        
        currentHp = _baseStats.hp;
        currentHarvestRate = _baseStats.harvestRate;
        currentFatigueRate = _baseStats.fatigueRate;
        
        idleState = new IdleState(this, _baseStats, _interface, _stateMachine);
        harvestState = new HarvestState(this, _baseStats, _interface, _stateMachine);
        vacationState = new VacationState(this, _baseStats, _interface, _stateMachine);
        dieState = new DieState(this, _baseStats, _interface, _stateMachine);
        mergeState = new MergeState(this, _baseStats, _interface, _stateMachine);
    }

    private void Start()
    {
        dead.AddListener(OnDead);
        _stateMachine.ChangeState(harvestState);
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

    public void RemoveFromIsland()
    {
        CurrentIslandController.RemoveMinion(this);
    }

    public void Refresh()
    {
        currentHp = _baseStats.hp;
        currentFatigueRate = _baseStats.fatigueRate;
        currentHarvestRate = _baseStats.harvestRate;
    }

    public bool IsWorking => _stateMachine.CurrentState == harvestState;
}
