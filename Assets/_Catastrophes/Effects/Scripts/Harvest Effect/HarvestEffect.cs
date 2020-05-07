using UnityEngine;

public class HarvestEffect : BaseTimedEffect
{
    private MinionStats _minionStats;
    
    public HarvestEffect(BaseScriptableEffect baseScriptableEffect, MinionStats minionStats) : base(baseScriptableEffect, minionStats)
    {
        _minionStats = minionStats;
    }

    protected override void ApplyEffect()
    {
        base.ApplyEffect();

        HarvestScriptable effect = (HarvestScriptable)BaseScriptableEffect;
        _minionStats.currentHarvestRate += effect.HarvestChange;
    }

    protected override void EndEffect()
    {
        base.EndEffect();
        
        HarvestScriptable effect = (HarvestScriptable)BaseScriptableEffect;
        _minionStats.currentHarvestRate -= effect.HarvestChange * EffectStacks;
        EffectStacks = 0;
    }
}
