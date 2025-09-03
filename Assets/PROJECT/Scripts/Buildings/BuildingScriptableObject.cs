using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Building")]
public class BuildingScriptableObject : ScriptableObject
{
    public string buildingName;
    public GameObject prefab;
    [Header("Upgrades")]
    public List<int> levelsHp = new();
    public List<int> levelsCost = new();
}
