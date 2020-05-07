using UnityEngine;

[CreateAssetMenu(menuName = "Effects/HarvestEffect")]
public class BaseScriptableHarvestEffect : BaseScriptableEffect
{
    public float HarvestRateIncrease;
    
    public override BaseTimedEffect InitializeEffect(MinionStats minionStats)
    {
        return new HarvestBaseTimedEffect(this, minionStats);
    }
}
