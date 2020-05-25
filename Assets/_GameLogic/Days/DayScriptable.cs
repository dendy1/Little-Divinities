using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Day")]
public class DayScriptable : ScriptableObject
{
    [Header("Real Time Settings")] 
    public int minutes;
    public int seconds;

    [Header("Desired Resources Count")] 
    public int desiredWood;
    public int desiredStone;
    public int desiredPower;
    public int desiredWater;
}
