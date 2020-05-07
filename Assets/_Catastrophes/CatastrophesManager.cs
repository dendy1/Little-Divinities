using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatastrophesManager : MonoBehaviourSingleton<CatastrophesManager>
{
    public List<IslandController> islandControllers;
    public float StartTime = 0f;
    public float EndTime = 5f;

    public void Start()
    {
        StartCoroutine(StartWeather());
    }

    private IEnumerator StartWeather()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(StartTime, EndTime));

            if (islandControllers.Count > 0)
            {
                var index = Random.Range(0, islandControllers.Count);
                
                if (islandControllers[index].catastrophe)
                    continue;

                islandControllers[index].catastrophe = true;
                islandControllers[index].StartRandomWeatherCondition();
            }
        }
    }
}
