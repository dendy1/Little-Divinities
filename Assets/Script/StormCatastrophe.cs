using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormCatastrophe : AbstractCatastrophe
{
    public float DurationTime = 2f;

    public override void EffectOnMinions(List<GameObject> minions)
    {
        StartCoroutine(EffectCoroutine(minions));
    }

    private IEnumerator EffectCoroutine(List<GameObject> minions)
    {
        yield return new WaitForSeconds(DurationTime);
        
        if (minions.Count > 1)
        {
            var count = Random.Range(1, 3);
            
            for (int i = 0; i < count; i++)
            {
                minions[Random.Range(0, minions.Count)].GetComponent<MinionDescriptor>().Die();
            }
        }
        else if (minions.Count == 1)
        {
            minions[0].GetComponent<MinionDescriptor>().Die();
        }

        gameObject.SetActive(false);
    }
}
