using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingUI : MonoBehaviour, IPointerClickHandler
{
    PlayerBuilder player;
    BuildingScriptableObject building;
    public TMP_Text indexText, nameText;
    int index;
    public void OnPointerClick(PointerEventData eventData)
    {
        return;
        player.selectedBuilding = building;
    }

    public void Setup(BuildingScriptableObject building, PlayerBuilder player, int index)
    {
        this.player = player;
        this.building = building;
        indexText.text = index.ToString() + " Cost: " + building.levelsCost[0];
        nameText.text = building.buildingName;
        this.index = index;
    }
    public void Update()
    {
       if (Input.GetKeyDown(KeyCode.Alpha0 + index))
       {
            if(player.selectedBuilding == building)
            {
                player.CancelBuilding();
            }
            else
            player.selectedBuilding = building;
        }
        
    }
}
