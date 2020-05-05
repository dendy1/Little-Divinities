using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatastrophesController : AbstractCatastrophe
{
    public List<IslandController> islandControllers;
    public float StartTime = 30f;
    public float EndTime = 80f;

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
                islandControllers[index].StartWeatherCondition();
            }
        }
    }
}
