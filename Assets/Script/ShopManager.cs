using UnityEngine;

public class ShopManager : MonoBehaviourSingleton<ShopManager>
{
    public void BuyMinion(GameObject prefab, IslandController islandController)
    {
        var stats = prefab.GetComponent<MinionBaseStats>();
        if (stats.cost >= GameManager.Instance.CrystalCount)
        {
            return;
        }

        GameManager.Instance.CrystalCount -= stats.cost;
        var minion = Instantiate(prefab);
        islandController.AddMinion(minion.GetComponent<MinionStats>());
    }
}
