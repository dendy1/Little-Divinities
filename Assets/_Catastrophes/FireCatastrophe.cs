using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCatastrophe : AbstractCatastrophe
{
    public float DurationTime = 2f;

    private float _timer;
    private bool _flag;
    private float _dieRate;

    public override void EffectOnMinions(List<MinionEffectable> minions)
    {
        _timer = 0;
        StartCoroutine(EffectCoroutine(minions));
    }
    
    private IEnumerator EffectCoroutine(List<MinionEffectable> minions)
    {
        while (_timer < DurationTime)
        {
            foreach (var minion in minions)
            {
                
            }

            _timer += Time.deltaTime;
            yield return null;
        }
        
        foreach (var minion in minions)
        {
            var descriptor = minion.GetComponent<MinionDescriptor>();
            descriptor.currentDieRate = descriptor.DieRate;
        }
        
        gameObject.SetActive(false);
    }
}
