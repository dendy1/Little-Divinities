using UnityEngine;

public class ShopManager : MonoBehaviourSingleton<ShopManager>
{
    public bool BuyMinion(GameObject prefab, IslandController islandController)
    {
        var stats = prefab.GetComponent<MinionStats>().BaseStats;
        
        if (stats.cost > GameManager.Instance.CrystalCount || islandController.MinionsCount + 1 > islandController.maxMinions)
        {
            return false;
        }

        GameManager.Instance.CrystalCount -= stats.cost;
        return true;
    }
}
