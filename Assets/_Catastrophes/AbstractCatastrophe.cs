using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCatastrophe : MonoBehaviour
{
    public float duration;
    protected float Timer;

    public virtual void EffectOnMinions(List<MinionEffectable> minions)
    {
        
    }
}
