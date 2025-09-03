using UnityEngine;

public class PlayerBuilder : MonoBehaviour
{
    public Transform cameraTransform;
    public BuildingScriptableObject selectedBuilding;
    public Building currentBuilding;
    public LayerMask groundMask;
    public LayerMask ignoringMask;
    public LayerMask buildingMask;
    bool canBuild;
    float rawRotate;
    private void Update()
    {
        rawRotate += Input.mouseScrollDelta.y;
        if (!GameManager.Instance.isWaveGoing)
        {
            if (selectedBuilding != null)
            {
                if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out var hit, 3, groundMask))
                {
                    if(currentBuilding == null)
                    {
                        currentBuilding = Instantiate(selectedBuilding.prefab, new Vector3(), Quaternion.identity).GetComponent<Building>();
                    }
                    float currentY = hit.point.y;

                    float snapX = Mathf.Round(hit.point.x / .125f) * .125f;
                    float snapZ = Mathf.Round(hit.point.z / .125f) * .125f;
                   
                    currentBuilding.transform.position = new Vector3(snapX, currentY, snapZ);

                    
                    currentBuilding.transform.rotation = Quaternion.Euler(0,Mathf.Round(cameraTransform.eulerAngles.y / 15) * 15 + Mathf.Round(rawRotate * 10 / 15) * 15, 0);
                    var overlaping = Physics.OverlapBox(currentBuilding.collider.transform.TransformPoint(currentBuilding.collider.center), Vector3.Scale(currentBuilding.collider.size * 0.5f, currentBuilding.collider.transform.lossyScale), currentBuilding.collider.transform.rotation, ~ignoringMask, QueryTriggerInteraction.Ignore);
                    canBuild = overlaping.Length == 0;
                    if (!canBuild)
                    {
                        currentBuilding.SetRedMat();
                    }
                    else
                    {
                        currentBuilding.SetGreenMat();
                    }
                }
                else
                {
                    if(currentBuilding != null)
                    Destroy(currentBuilding.gameObject);
                }
            }
            else
            {
                if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out var hit, 3, buildingMask))
                {
                        var b = hit.transform.GetComponentInParent<Building>();
                        if (Input.GetMouseButtonDown(0))
                        {
                             b.Upgrade();  
                        }
                    
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(currentBuilding != null && canBuild)
            {
                if (currentBuilding.CheckMoney())
                {
                    GameManager.Instance.money -= currentBuilding.building.levelsCost[0];
                    currentBuilding.GetComponent<Building>().Setup();
                    currentBuilding = null;
                }
                
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelBuilding();
        }
    }
    public void CancelBuilding()
    {
        if (currentBuilding != null)
        {
            Destroy(currentBuilding.gameObject);
            selectedBuilding = null;
        }
    }
}
