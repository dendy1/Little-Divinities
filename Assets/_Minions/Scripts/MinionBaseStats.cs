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
}
