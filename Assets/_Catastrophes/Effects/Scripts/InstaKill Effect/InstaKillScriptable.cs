using UnityEngine;

[CreateAssetMenu(menuName = "Effects/InstaKillEffect")]
public class InstaKillScriptable : BaseScriptableEffect
{
    public float InstaKillChance;
    
    public override BaseTimedEffect InitializeEffect(MinionStats minionStats)
    {
        return new InstaKillEffect(this, minionStats);
    }
}
