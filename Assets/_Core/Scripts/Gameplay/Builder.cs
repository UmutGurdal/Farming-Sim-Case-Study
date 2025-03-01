using UnityEngine;
using Grid = Muchwood.Grid;

public class Builder : MonoBehaviour
{
    [SerializeField] private Building[] buildings;

    private Building SelectedBuilding;

    private Building instantiatedBuilding;
    private Grid lastInteractedGrid;

    private void OnEnable()
    {
        BuildingSelector.OnBuildingSelected += OnBuildingSelected;
    }

    private void OnDisable()
    {
        BuildingSelector.OnBuildingSelected -= OnBuildingSelected;
    }

    private void Update()
    {
        Debug.Log("TouchCount: " + Input.touchCount);
    }

    private void FixedUpdate()
    {
        if (SelectedBuilding == null)
            return;

        if (Input.touchCount <= 0)
            return;

        var touch = Input.GetTouch(0);

        var ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));

        //Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {
            if (hit.transform.TryGetComponent<Grid>(out Grid grid))
            {
                if (GameManager.ins.GridManager.CanBuild(grid.Coordinate, SelectedBuilding.BuildingSize) == false)
                    return;

                lastInteractedGrid = grid;

                if (instantiatedBuilding == null)
                    instantiatedBuilding = Instantiate(SelectedBuilding);

                instantiatedBuilding.transform.position = grid.transform.position;

                if (touch.phase == TouchPhase.Ended)
                {
                    GameManager.ins.GridManager.Constructed(lastInteractedGrid.Coordinate, instantiatedBuilding.BuildingSize);
                    instantiatedBuilding = null;
                    return;
                }
            }
        }

        if(touch.phase == TouchPhase.Ended)
        {
            Destroy(instantiatedBuilding.gameObject);
            instantiatedBuilding = null;
        }
    }

    private void OnBuildingSelected(int selectedID) 
    {
        SelectedBuilding = buildings[selectedID];
    }
}
