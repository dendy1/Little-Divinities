using UnityEngine;

public abstract class BaseTimedEffect
{
    protected float Duration;
    protected int EffectStacks;
    public readonly MinionStats MinionStats;
    
    
    public BaseScriptableEffect BaseScriptableEffect { get; }
    public bool isFinished;
    public bool isApplied;

    public BaseTimedEffect(BaseScriptableEffect baseScriptableEffect, MinionStats minionStats)
    {
        BaseScriptableEffect = baseScriptableEffect;
        MinionStats = minionStats;
    }

    public void Tick(float delta)
    {
        Duration -= delta;

        if (Duration <= 0)
        {
            EndEffect();
            isFinished = true;
        }
    }

    public void Activate()
    {
        if (BaseScriptableEffect.isEffectStacked || Duration <= 0)
        {
            ApplyEffect();
            EffectStacks++;
        }

        if (BaseScriptableEffect.isDurationStacked || Duration <= 0)
        {
            Duration += BaseScriptableEffect.duration;
        }
        
        if (!isApplied)
            ApplyEffect();
    }

    protected virtual void ApplyEffect()
    {
        isApplied = true;
    }

    protected virtual void EndEffect()
    {
        if (!isApplied) return;

        isApplied = false;
    }
}
