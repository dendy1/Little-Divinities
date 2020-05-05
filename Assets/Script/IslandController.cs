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
    
    [Header("CATASTROPHE OBJECTS")] 
    public List<AbstractCatastrophe> catastropheList;

    public GameObject rainObject;
    public GameObject fireObject;
    public GameObject stormObject;
    
    
    [Header("ISLAND SETTINGS")] 
    public int maxResources;
    public int maxMinions;

    public bool rainUpgrade, fireUpgrade, stormUpgrade;
    
    private List<GameObject> Minions { get; set; }
    public int MinionsCount { get; set; }

    public void AddMinion(GameObject minion)
    {
        var descriptor = minion.GetComponent<MinionDescriptor>();
        descriptor.dead.AddListener(() => Minions.Remove(minion));
        descriptor.dead.AddListener(() => MinionsCount--);
        Minions.Add(minion);
    }

    public void RemoveMinion(GameObject minion)
    {
        minion.GetComponent<MinionDescriptor>().ResetParameters();
        Minions.Remove(minion);
    }

    private void Start()
    {
        Minions = new List<GameObject>();
        dobicha = false;
        StartCoroutine(Kostil());
    }

    public void TurnOutline(bool val)
    {
        foreach (Transform child in transform.GetChild(0))
        {
            var outline = child.GetComponent<Outline>();
            if (outline)
                outline.enabled = val;
        }
    }

    public void StartWeatherCondition()
    {
        if (catastropheList.Count > 0)
        {
            var catastrophe = Random.Range(0, catastropheList.Count);
            catastropheList[catastrophe].gameObject.SetActive(true);
            catastropheList[catastrophe].EffectOnMinions(Minions);
        }
    }

    public GameObject MergeBrothers(MinionDescriptor minionDescriptor)
    {
        using (IEnumerator<GameObject> enumrator = Minions.GetEnumerator())
        {
            while (enumrator.MoveNext())
            {
                var minion = enumrator.Current;     
                var mind = minion.GetComponent<MinionDescriptor>();
                if (mind.type == minionDescriptor.type && mind.currentPower == minionDescriptor.currentPower)
                {
                    Transform transform1;
                    var super = Instantiate(mind.gameObject, (transform1 = mind.transform).position, transform1.rotation, transform1.parent);
                    super.transform.localScale = Vector3.one*0.2f;
                    super.GetComponent<MinionController>().MainIsland =
                        mind.GetComponent<MinionController>().MainIsland;
                    var newMini = super.GetComponent<MinionDescriptor>();
                    super.GetComponent<EnemyHealthController>().StartHealth = (int)(mind.currentMaxHP * 2f);
                    newMini.SetParameters(mind.type, mind.currentMaxHP * 3f, mind.currentPower * 3f);
                    newMini.WhenPlacedOnMerging();

                    super.GetComponent<MinionController>().MainIsland.GetComponent<IslandController>().MinionsCount++;

                    mind.Die();
                    minionDescriptor.Die();

                    return super;
                }
            }
            
            return null;
        }
    }

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
