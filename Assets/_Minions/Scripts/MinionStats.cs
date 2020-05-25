using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MinionInterface))]
public class MinionStats : MonoBehaviour
{
    [SerializeField] private MinionBaseStats baseStats;

    public IdleState idleState;
    public HarvestState harvestState;
    public VacationState vacationState;
    public DieState dieState;
    public MergeState mergeState;
        
    public float currentHp;
    public float currentHarvestRate;
    public float currentFatigueRate;

    private StateMachine _stateMachine;
    private MinionInterface _interface;
    
    public StateMachine StateMachine => _stateMachine;
    public MinionBaseStats BaseStats => baseStats;
    public MinionInterface Interface => _interface;
    
    public IslandController mainIslandController;
    public IslandController currentIslandController;

    public UnityEvent dead = new UnityEvent();

    public bool Merged = false;
    
    private void Awake()
    {
        _stateMachine = new StateMachine();
        _interface = GetComponent<MinionInterface>();
        
        currentHp = baseStats.hp;
        currentHarvestRate = baseStats.harvestRate;
        currentFatigueRate = baseStats.fatigueRate;
        
        idleState = new IdleState(this);
        harvestState = new HarvestState(this);
        vacationState = new VacationState(this);
        dieState = new DieState(this);
        mergeState = new MergeState(this);
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
        Destroy(gameObject);
    }

    public void RemoveFromIsland()
    {
        currentIslandController.RemoveMinion(this);
    }

    public void Refresh()
    {
        currentHp = baseStats.hp;
        currentFatigueRate = baseStats.fatigueRate;
        currentHarvestRate = baseStats.harvestRate;
    }

    public void MergeWith(MinionBaseStats other)
    {
        currentHp = (baseStats.hp + other.hp) * 0.5f * 3f;
        currentHarvestRate = (baseStats.harvestRate + other.harvestRate) * 0.5f * 2.5f;
        currentFatigueRate = (baseStats.fatigueRate + other.fatigueRate) * 0.5f * 1.5f;
    }

    public bool IsWorking => _stateMachine.CurrentState == harvestState;
}
