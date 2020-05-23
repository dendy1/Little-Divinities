using UnityEngine;

public class FatigueEffect : BaseTimedEffect
{    
    public FatigueEffect(BaseScriptableEffect baseScriptableEffect, MinionStats minionStats) : base(baseScriptableEffect, minionStats)
    {
    }

    protected override void ApplyEffect()
    {
        base.ApplyEffect();
        
        FatigueScriptable effect = (FatigueScriptable)BaseScriptableEffect;
        MinionStats.currentFatigueRate += effect.FatigueChange;
    }

    protected override void EndEffect()
    {
        base.EndEffect();
        
        FatigueScriptable effect = (FatigueScriptable)BaseScriptableEffect;
        MinionStats.currentFatigueRate -= effect.FatigueChange * EffectStacks;
        EffectStacks = 0;
    }
}
