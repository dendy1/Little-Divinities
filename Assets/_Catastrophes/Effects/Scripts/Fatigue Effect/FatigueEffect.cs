using UnityEngine;

public class FatigueEffect : BaseTimedEffect
{
    private MinionStats _minionStats;
    
    public FatigueEffect(BaseScriptableEffect baseScriptableEffect, MinionStats minionStats) : base(baseScriptableEffect, minionStats)
    {
        _minionStats = minionStats;
    }

    protected override void ApplyEffect()
    {
        base.ApplyEffect();
        
        FatigueScriptable effect = (FatigueScriptable)BaseScriptableEffect;
        _minionStats.currentFatigueRate += effect.FatigueChange;
    }

    protected override void EndEffect()
    {
        base.EndEffect();
        
        FatigueScriptable effect = (FatigueScriptable)BaseScriptableEffect;
        _minionStats.currentFatigueRate -= effect.FatigueChange * EffectStacks;
        EffectStacks = 0;
    }
}
