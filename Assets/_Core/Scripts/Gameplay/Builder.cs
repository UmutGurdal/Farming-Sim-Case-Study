using UnityEngine;
using Grid = Muchwood.Grid;
using System;

public class Builder : MonoBehaviour
{
    public static event Action OnBuildingDestroyed;

    [SerializeField] private LayerMask gridLayer;
    [SerializeField] private Building[] buildings;

    private Building SelectedBuilding;

    private Building instantiatedBuilding;
    private Grid lastInteractedGrid;

    private void OnEnable()
    {
        BuildingSelector.OnBuildingSelected += OnBuildingSelected;
        Building.OnBuildingHold += OnBuildingHold;
    }

    private void OnDisable()
    {
        BuildingSelector.OnBuildingSelected -= OnBuildingSelected;
        Building.OnBuildingHold -= OnBuildingHold;
    }

    private void FixedUpdate()
    {
        if (SelectedBuilding == null)
            return;

        if (Input.touchCount <= 0)
            return;

        var touch = Input.GetTouch(0);

        var ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));


        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, gridLayer))
        {
            if (hit.transform.TryGetComponent<Grid>(out Grid grid))
            {
                if (GameManager.ins.GridManager.CanBuild(grid.Coordinate, SelectedBuilding.BuildingSize) == false)
                    return;

                lastInteractedGrid = grid;

                if (instantiatedBuilding == null)
                    instantiatedBuilding = Instantiate(SelectedBuilding);

                instantiatedBuilding.gameObject.SetActive(true);
                instantiatedBuilding.transform.position = grid.transform.position;

                if (touch.phase == TouchPhase.Ended)
                {
                    GameManager.ins.GridManager.Constructed(lastInteractedGrid.Coordinate, instantiatedBuilding.BuildingSize);
                    instantiatedBuilding.ConstructedOnGrid = lastInteractedGrid;
                    instantiatedBuilding = null;
                    SelectedBuilding = null;
                    return;
                }
            }

        }

        else
            instantiatedBuilding?.gameObject.SetActive(false);

        if (touch.phase == TouchPhase.Ended)
        {
            if (instantiatedBuilding == null)
                return;

            Destroy(instantiatedBuilding.gameObject);
            instantiatedBuilding = null;
            SelectedBuilding = null;
            OnBuildingDestroyed?.Invoke();
        }
    }

    private void OnBuildingSelected(int selectedID)
    {
        SelectedBuilding = buildings[selectedID];
    }

    private void OnBuildingHold(Building building)
    {
        SelectedBuilding = building;
        instantiatedBuilding = building;
    }
}
