using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


/**
 * TODO: Extend GameObject to have a placeable object that contains data around dir state and next in line for belts
 * Also need some way to handle the entire grid state, some massive 2d array to handle maps belt state and work out contigious belts and belt sections
 */

public class BuildingGridPlacer : BuildingPlacer
{
   

    public float cellSize;
    public Vector2 gridOffset;
    public Renderer gridRenderer;

    public string nameString;
    public Transform prefab;
    public Transform visual;

#if UNITY_EDITOR
    private void OnValidate() {
        UpdateGridVisual();
    }
#endif

    private void Start() {
        UpdateGridVisual();
        EnableGridVisual(false);
    }

    private void Update() {
        if (buildingPrefab != null) {
            // in build mode

            // cancel operation
            if (Input.GetMouseButtonDown(1)) {
                Destroy(toBuild);
                toBuild = null;
                buildingPrefab = null;
                EnableGridVisual(false);
                return;
            }

            // hide preview when hovering UI
            if (EventSystem.current.IsPointerOverGameObject()) {
                if (toBuild.activeSelf) toBuild.SetActive(false);

            } else if (!toBuild.activeSelf) {
                toBuild.SetActive(true);
            }

            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            BuildingManager buildingManager = toBuild.GetComponent<BuildingManager>();

            // rotate preview with Spacebar
            if (Input.GetKeyDown(KeyCode.R)) {
                buildingManager.dir = BuildingManager.GetNextDir(buildingManager.dir);
                toBuild.transform.Rotate(Vector3.up, 90);
            }

            if (Physics.Raycast(ray, out hit, 1000f, groundLayerMask)) {
                if (!toBuild.activeSelf) {
                    toBuild.SetActive(true);
                }
                toBuild.transform.position = ClampToNearest(hit.point, cellSize);

                if (Input.GetMouseButtonDown(0)) {
                    if (EventSystem.current.IsPointerOverGameObject()) {
                        if (toBuild.activeSelf) {
                            toBuild.SetActive(false);
                            Destroy(toBuild);
                            toBuild = null;
                            buildingPrefab = null;
                            EnableGridVisual(false);
                            return;
                        }
                    } 

                buildingManager = toBuild.GetComponent<BuildingManager>();
                    if (buildingManager.hasValidPlacement) {
                        buildingManager.SetPlacementMode(PlacementMode.Fixed);
                        // need to store coordinate of buildable here, this way we can keep track of belts.
                        // belts need to update on a fixed tick rate
                        // we need to scan and check belt pathing
                        buildingManager.x = toBuild.transform.position.x;
                        buildingManager.z = toBuild.transform.position.z;


                        // exit build mode
                        buildingPrefab = null;
                        toBuild = null;
                        EnableGridVisual(false);
                    }

                }
            } else if (toBuild.activeSelf) {
                toBuild.SetActive(false);
            }
        }
    }

    protected override void PrepareBuilding() {
        base.PrepareBuilding();
        EnableGridVisual(true);
    }

    private Vector3 ClampToNearest(Vector3 pos, float threshold) {
        // snapping algorithm
        float t = 1f / threshold;
        Vector3 v = ((Vector3)Vector3Int.FloorToInt(pos * t)) / t;

        float s = threshold * 0.5f;
        v.x += s + gridOffset.x; // (recenter in middle of cells)
        v.z += s + gridOffset.y;
        v.y = 0;


        return v;
    }

    private void EnableGridVisual(bool on) {
        if (gridRenderer == null) {
            return;
        }
        gridRenderer.gameObject.SetActive(on);
        // gridRenderer.sharedMaterial.SetColor("_Grid_Color", new Color(1, 2, 3, 0.1f));

    }

    private void UpdateGridVisual() {
        if (gridRenderer == null) {
            return;
        }
        gridRenderer.sharedMaterial.SetVector("_Cell_Size", new Vector4(cellSize, cellSize, 0, 0));
    }

}
