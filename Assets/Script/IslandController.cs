using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Outline = cakeslice.Outline;
using Random = UnityEngine.Random;

public class IslandController : MonoBehaviour
{
    public bool not_icon;
    
    public GameObject iconRes;
    public bool dobicha;
    
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
    
    public int MinionsCount => _minions.Count;

    public bool catastrophe;

    public void AddMinion(MinionStats minionStats)
    {
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
        _minions.Remove(minionStats);
        
        var effectable = minionStats.GetComponent<MinionEffectable>();
        if (effectable)
        {
            _minionsEffectable.Remove(effectable);
        }
    }

    private void Start()
    {
        _minions = new List<MinionStats>();
        _minionsEffectable = new List<MinionEffectable>();
        dobicha = false;
        StartCoroutine(Kostil());
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
            catastropheList[catastrophe].EffectOnMinions(_minionsEffectable);
        }
    }
    

    // public GameObject MergeBrothers(MinionDescriptor minionDescriptor)
    // {
    //     using (IEnumerator<GameObject> enumrator = _minions.GetEnumerator())
    //     {
    //         while (enumrator.MoveNext())
    //         {
    //             var minion = enumrator.Current;     
    //             var mind = minion.GetComponent<MinionDescriptor>();
    //             if (mind.type == minionDescriptor.type && mind.currentPower == minionDescriptor.currentPower)
    //             {
    //                 Transform transform1;
    //                 var super = Instantiate(mind.gameObject, (transform1 = mind.transform).position, transform1.rotation, transform1.parent);
    //                 super.transform.localScale = Vector3.one*0.2f;
    //                 super.GetComponent<MinionController>().MainIsland =
    //                     mind.GetComponent<MinionController>().MainIsland;
    //                 var newMini = super.GetComponent<MinionDescriptor>();
    //                 super.GetComponent<EnemyHealthController>().StartHealth = (int)(mind.currentMaxHP * 2f);
    //                 newMini.SetParameters(mind.type, mind.currentMaxHP * 3f, mind.currentPower * 3f);
    //                 newMini.WhenPlacedOnMerging();
    //
    //                 super.GetComponent<MinionController>().MainIsland.GetComponent<IslandController>().MinionsCount++;
    //
    //                 mind.Die();
    //                 minionDescriptor.Die();
    //
    //                 return super;
    //             }
    //         }
    //         
    //         return null;
    //     }
    // }

    private void Update()
    {
        if (!not_icon)
        {
            if (dobicha)
            {
                iconRes.GetComponent<Animator>().SetBool("active", true);
            }

            if (!dobicha)
            {
                iconRes.GetComponent<Animator>().SetBool("active", false);
            }
        }
    }

    IEnumerator Kostil()
    {
        yield return new WaitForSeconds(2);
        dobicha = false;
        StartCoroutine(Kostil());
    }
}
