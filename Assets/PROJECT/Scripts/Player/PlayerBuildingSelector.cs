using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBuildingSelector : MonoBehaviour
{
    BuildingScriptableObject[] buildings;
    public PlayerBuilder playerBuilder;
    public GameObject itemPrefab;
    public Transform itemParent;
    public List<BuildingUI> uis = new(); 
    private void Start()
    {
        buildings = Resources.LoadAll<BuildingScriptableObject>("");
        for (int i = 0; i < buildings.Length; i++)
        {
            BuildingScriptableObject building = buildings[i];
            var item = Instantiate(itemPrefab, itemParent);
            item.GetComponent<BuildingUI>().Setup(building, playerBuilder, i + 1);
        }
    }
    public void Update()
    {
        
    }
}
