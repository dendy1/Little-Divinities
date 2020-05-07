using UnityEngine;

[CreateAssetMenu(menuName = "Effects/HarvestEffect")]
public class HarvestScriptable : BaseScriptableEffect
{
    public float HarvestChange;
    
    public override BaseTimedEffect InitializeEffect(MinionStats minionStats)
    {
        return new HarvestEffect(this, minionStats);
    }
}
