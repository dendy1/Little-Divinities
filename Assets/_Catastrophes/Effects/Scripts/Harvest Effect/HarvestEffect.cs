using UnityEngine;

public class HarvestEffect : BaseTimedEffect
{
    public HarvestEffect(BaseScriptableEffect baseScriptableEffect, MinionStats minionStats) : base(baseScriptableEffect, minionStats)
    {
    }

    protected override void ApplyEffect()
    {
        base.ApplyEffect();

        HarvestScriptable effect = (HarvestScriptable)BaseScriptableEffect;
        MinionStats.currentHarvestRate += effect.HarvestChange;
    }

    protected override void EndEffect()
    {
        base.EndEffect();
        
        HarvestScriptable effect = (HarvestScriptable)BaseScriptableEffect;
        MinionStats.currentHarvestRate -= effect.HarvestChange * EffectStacks;
        EffectStacks = 0;
    }
}
