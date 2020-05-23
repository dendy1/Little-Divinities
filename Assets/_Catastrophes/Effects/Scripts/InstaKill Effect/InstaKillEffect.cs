using UnityEngine;

public class InstaKillEffect : BaseTimedEffect
{    
    public InstaKillEffect(BaseScriptableEffect baseScriptableEffect, MinionStats minionStats) : base(baseScriptableEffect, minionStats)
    {
    }

    protected override void ApplyEffect()
    {
        base.ApplyEffect();

        InstaKillScriptable effect = (InstaKillScriptable)BaseScriptableEffect;

        if (Random.Range(0f, 100f) <= effect.InstaKillChance)
        {
            MinionStats.dead?.Invoke();
        }
    }
}
