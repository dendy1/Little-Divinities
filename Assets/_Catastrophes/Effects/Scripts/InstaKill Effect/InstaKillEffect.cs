using UnityEngine;

public class InstaKillEffect : BaseTimedEffect
{
    private MinionStats _minionStats;
    
    public InstaKillEffect(BaseScriptableEffect baseScriptableEffect, MinionStats minionStats) : base(baseScriptableEffect, minionStats)
    {
        _minionStats = minionStats;
    }

    protected override void ApplyEffect()
    {
        base.ApplyEffect();

        InstaKillScriptable effect = (InstaKillScriptable)BaseScriptableEffect;

        if (Random.Range(0f, 100f) <= effect.InstaKillChance)
        {
            _minionStats.dead?.Invoke();
        }
    }
}
