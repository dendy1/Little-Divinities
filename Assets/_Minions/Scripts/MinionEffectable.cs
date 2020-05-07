using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MinionStats))]
public class MinionEffectable : MonoBehaviour
{
    public MinionStats MinionStats { get; private set; }
    
    private readonly Dictionary<BaseScriptableEffect, BaseTimedEffect> _effects = new Dictionary<BaseScriptableEffect, BaseTimedEffect>();

    private void Start()
    {
        MinionStats = GetComponent<MinionStats>();
    }

    private void Update()
    {
        foreach (var effect in _effects.Values.ToList())
        {
            effect.Tick(Time.deltaTime);

            if (effect.isFinished)
            {
                _effects.Remove(effect.BaseScriptableEffect);
            }
        }
    }

    public void AddEffect(BaseTimedEffect effect)
    {
        if (_effects.ContainsKey(effect.BaseScriptableEffect))
        {
            _effects[effect.BaseScriptableEffect].Activate();
        }
        else
        {
            _effects.Add(effect.BaseScriptableEffect, effect);
            effect.Activate();
        }
    }
}
