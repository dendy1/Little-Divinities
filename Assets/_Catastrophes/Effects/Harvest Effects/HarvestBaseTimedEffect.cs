public class HarvestBaseTimedEffect : BaseTimedEffect
{
    private MinionStats _minionStats;
    
    public HarvestBaseTimedEffect(BaseScriptableEffect baseScriptableEffect, MinionStats minionStats) : base(baseScriptableEffect, minionStats)
    {
        _minionStats = minionStats;
    }

    protected override void ApplyEffect()
    {
        BaseScriptableHarvestEffect effect = (BaseScriptableHarvestEffect)BaseScriptableEffect;
        _minionStats.currentHarvestRate += effect.HarvestRateIncrease;
    }

    protected override void EndEffect()
    {
        BaseScriptableHarvestEffect effect = (BaseScriptableHarvestEffect)BaseScriptableEffect;
        _minionStats.currentHarvestRate -= effect.HarvestRateIncrease * EffectStacks;
        EffectStacks = 0;
    }
}
