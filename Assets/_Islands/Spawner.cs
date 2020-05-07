using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Ground Settings")] 
    [SerializeField] private Transform groundPlane;

    [Header("Spawn Zone Settings")] 
    [SerializeField] private bool preview;
    [SerializeField] private CustomMath.Borders innerBorders;
    [SerializeField] private CustomMath.Borders externalBorders;

    [Header("Spawner Settings")] 
    public GameObject minionPrefab;
    [SerializeField] private bool isEnabled;

    
    public GameObject SpawnMinion()
    {
        if (!isEnabled) return null;
        
        var randomPosition = RandomVector3();
        return Instantiate(minionPrefab, randomPosition, Quaternion.identity);
    }

    // returns random position within borders
    public Vector3 RandomVector3()
    {
        float randomX = 0, randomZ = 0;
        if (Random.Range(0, 2) < 1)
        {
            randomX = CustomMath.RandomValueFromRanges(
                new CustomMath.Range(-externalBorders.left, -innerBorders.left),
                new CustomMath.Range(innerBorders.right, externalBorders.right));
            randomZ = CustomMath.RandomValueFromRanges(new CustomMath.Range(-externalBorders.bottom, externalBorders.top));
        }
        else
        {
            randomX = CustomMath.RandomValueFromRanges(new CustomMath.Range(-externalBorders.left, externalBorders.right));
            randomZ = CustomMath.RandomValueFromRanges(
                new CustomMath.Range(-externalBorders.bottom, -innerBorders.bottom),
                new CustomMath.Range(innerBorders.top, externalBorders.top));
        }
        
        return new Vector3(randomX, 0f, randomZ) + groundPlane.position;
    }

    private void OnDrawGizmosSelected()
    {
        if (!preview)
            return;
        
        innerBorders.Normalize(externalBorders);

        Vector3[] inner = innerBorders.GetPoints(groundPlane.position);
        Vector3[] external = externalBorders.GetPoints(groundPlane.position);

        var lineWidth = 5;
        var lineColor = Color.blue;
        
        GizmosExtensions.DrawBorders(lineColor, lineWidth, inner, external);
    }
}
