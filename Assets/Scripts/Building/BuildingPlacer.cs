using UnityEngine;
using UnityEngine.EventSystems;


public class BuildingPlacer : MonoBehaviour
{
    public static BuildingPlacer instance;
    public LayerMask groundLayerMask;

    protected GameObject buildingPrefab;
    protected GameObject toBuild;

    protected Camera mainCamera;
    protected Ray ray;
    protected RaycastHit hit;



    private void Awake() {
        instance = this; // singleton
        mainCamera = Camera.main; 
        buildingPrefab = null;
    }

    private void Update() {
        if (buildingPrefab != null) {
            // in build mode
            
            // cancel operation
            if (Input.GetMouseButtonDown(1)) {
                Destroy(toBuild);
                toBuild = null;
                buildingPrefab = null;
                return;
            }

            // hide preview when hovering UI
            if (EventSystem.current.IsPointerOverGameObject()) {
                if (toBuild.activeSelf) toBuild.SetActive(false);

            } else if (!toBuild.activeSelf) {
                toBuild.SetActive(true);
            }

            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f, groundLayerMask)) {
                if (!toBuild.activeSelf) toBuild.SetActive(true);
                toBuild.transform.position = hit.point;

                if (Input.GetMouseButtonDown(0)) {
                    BuildingManager buildingManager = toBuild.GetComponent<BuildingManager>();
                    if (buildingManager.hasValidPlacement) {
                        buildingManager.SetPlacementMode(PlacementMode.Fixed);

                        // exit build mode
                        buildingPrefab = null;
                        toBuild = null;
                    }
                    
                }
            } else if (toBuild.activeSelf) toBuild.SetActive(false);
        } 
    }

    public void setBuildingPrefab(GameObject prefab) {
        buildingPrefab = prefab;
        EventSystem.current.SetSelectedGameObject(null);
        PrepareBuilding();


    }

    protected virtual void PrepareBuilding() {

        // destroy if one already exists
        if (toBuild) Destroy(toBuild);

        toBuild = Instantiate(buildingPrefab);
        toBuild.SetActive(false);

        BuildingManager buildingManager = toBuild.GetComponent<BuildingManager>();
        buildingManager.isFixed = false;
        buildingManager.SetPlacementMode(PlacementMode.Valid);

    }
}
