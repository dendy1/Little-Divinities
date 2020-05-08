using UnityEngine;

[CreateAssetMenu(menuName = "Situation")]
public class SituationScriptable : ScriptableObject
{
    [Header("Situation Text Fields")]
    public string name;
    [TextArea]
    public string description;
    public string goodChoice;
    public string badChoice;

    [Header("Good choice resources")] 
    public int goodChoiceWood;
    public int goodChoiceStone;
    public int goodChoiceCrystal;
    public int goodChoicePower;
    public int goodChoiceWater;
    
    
    [Header("Good choice human stats")] 
    public int goodChoiceEconomy;
    public int goodChoiceFood;
    public int goodChoiceHappiness;
    public int goodChoiceHealth;
    

    [Header("Bad choice resources")] 
    public int badChoiceWood;
    public int badChoiceStone;
    public int badChoiceCrystal;
    public int badChoicePower;
    public int badChoiceWater;
    
    [Header("Bad choice human stats")] 
    public int badChoiceEconomy;
    public int badChoiceFood;
    public int badChoiceHappiness;
    public int badChoiceHealth;
}
