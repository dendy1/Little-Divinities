using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Outline = cakeslice.Outline;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Spawner))]
public class IslandController : MonoBehaviour
{
    [Header("CATASTROPHES LIST")] 
    public List<AbstractCatastrophe> catastropheList;

    [Header("CATASTROPHES OBJECTS")] 
    public GameObject rainObject;
    public GameObject fireObject;
    public GameObject stormObject;

    [Header("ISLAND SETTINGS")] 
    public int maxResources;
    public int maxMinions;

    public bool rainUpgrade, fireUpgrade, stormUpgrade;

    private List<MinionStats> _minions;
    private List<MinionEffectable> _minionsEffectable; 
    protected Camera MainCamera;
    
    public int MinionsCount => _minions.Count;
    public bool catastropheEnabled;

    public List<MinionStats> Minions => _minions;
    public List<MinionEffectable> MinionsEffectable => _minionsEffectable;
    public Spawner Spawner { get; private set; }
    
    protected MinionStats Selected
    {
        get => TranslationManager.Instance.SelectedMinion;
        set => TranslationManager.Instance.SelectedMinion = value;
    }
    
    private void Awake()
    {
        Spawner = GetComponent<Spawner>();
        MainCamera = Camera.main;
    }

    private void Start()
    {
        _minions = new List<MinionStats>();
        _minionsEffectable = new List<MinionEffectable>();
    }
    
    private void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (TranslationManager.Instance.SelectedMinion && TranslationManager.Instance.SelectedMinion.mainIslandController == this)
        {
            var ray = MainCamera.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Selected.RemoveFromIsland();
                Selected.StateMachine.ChangeState(Selected.harvestState);
                Selected.transform.position = hit.point;
                AddMinion(Selected);
                Selected = null;
                GameManager.Instance.SetActiveIslandsOutline(false);
            }
        }
    }

    public void AddMinion(MinionStats minionStats)
    {
        if (!minionStats.mainIslandController)
            minionStats.mainIslandController = minionStats.currentIslandController = this;
        else
            minionStats.currentIslandController = this;
        
        minionStats.dead.AddListener(() => _minions.Remove(minionStats));
        _minions.Add(minionStats);

        var effectable = minionStats.GetComponent<MinionEffectable>();
        if (effectable)
        {
            _minionsEffectable.Add(effectable);
        }
    }

    public void RemoveMinion(MinionStats minionStats)
    {
        var effectable = minionStats.GetComponent<MinionEffectable>();
        if (effectable)
        {
            _minionsEffectable.Remove(effectable);
        }
        
        _minions.Remove(minionStats);
    }
    
    public void SetActiveOutline(bool active)
    {
        foreach (Transform child in transform.GetChild(0))
        {
            var outline = child.GetComponent<Outline>();
            
            if (outline)
                outline.enabled = active;
        }
    }

    public void StartRandomWeatherCondition()
    {
        if (catastropheList.Count > 0)
        {
            var catastrophe = Random.Range(0, catastropheList.Count);
            catastropheList[catastrophe].gameObject.SetActive(true);
            catastropheList[catastrophe].StartCatastrophe();
        }
    }

    public int WorkingMinions => _minions.Count(m => m.IsWorking);
}
