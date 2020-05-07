using UnityEngine;

public enum ResourceType
{
    Wood,
    Stone,
    Shards,
    Power,
    Water
}

public class MinionBaseStats : MonoBehaviour
{
    [Header("Minion stats")]
    public float hp;
    public float harvestRate;
    public float fatigueRate;
    public ResourceType resourceTypeProduction;
    
    [Header("Shop parameters")]
    public float cost;
    public Sprite iconSprite;

    public void MergeWith(MinionBaseStats other)
    {
        hp = (hp + other.hp) * 0.5f * 3f;
        harvestRate = (harvestRate + other.harvestRate) * 0.5f * 2.5f;
        fatigueRate = (fatigueRate + other.fatigueRate) * 0.5f * 1.5f;
    }
}
