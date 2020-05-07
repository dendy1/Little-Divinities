using UnityEngine;

[CreateAssetMenu(menuName = "Effects/FatigueEffect")]
public class FatigueScriptable : BaseScriptableEffect
{
    public float FatigueChange;
    
    public override BaseTimedEffect InitializeEffect(MinionStats minionStats)
    {
        return new FatigueEffect(this, minionStats);
    }
}
