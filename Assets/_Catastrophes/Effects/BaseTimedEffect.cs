public abstract class BaseTimedEffect
{
    protected float Duration;
    protected int EffectStacks;
    protected readonly MinionStats MinionStats;
    
    public BaseScriptableEffect BaseScriptableEffect { get; }
    public bool isFinished;

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
    }

    protected virtual void ApplyEffect()
    {
        
    }

    protected virtual void EndEffect()
    {
        
    }
}
