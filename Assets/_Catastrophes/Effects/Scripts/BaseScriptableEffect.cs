using UnityEngine;

public abstract class BaseScriptableEffect : ScriptableObject
{
    public float duration;
    public bool isDurationStacked;
    public bool isEffectStacked;

    public abstract BaseTimedEffect InitializeEffect(MinionStats minionStats);
}
