using Muchwood.Utils;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Grid = Muchwood.Grid;

public class Building : MonoBehaviour
{
    public static event Action<Building> OnBuildingHold;

    public Vector2Int BuildingSize;

    float interactionTime;
    bool canSelected;

    [ReadOnly] public Grid ConstructedOnGrid;

    [SerializeField] private LayerMask buildingLayer;

    internal virtual void OnEnable()
    {
        BuildingSelector.OnBuildingSelected += OnBuildingSelected;
        GridManager.OnConstructed += OnConstructed;
    }

    internal virtual void OnDisable()
    {
        BuildingSelector.OnBuildingSelected -= OnBuildingSelected;
        GridManager.OnConstructed -= OnConstructed;
    }

    private void FixedUpdate()
    {
        if (canSelected == false)
            return;

        if (Input.touchCount <= 0)
            return;

        Touch touch = Input.GetTouch(0);
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildingLayer))
        {
            if(hit.transform.TryGetComponent<Building>(out Building building))
            {
                if(building != this)
                { 
                    interactionTime = 0;
                    return;
                }

                interactionTime += Time.fixedDeltaTime;

                if (interactionTime > 0.2f)
                    BuildingHold();

                if (touch.phase == TouchPhase.Ended)
                {
                    if (interactionTime < 0.2f)
                        BuildingClicked();

                    interactionTime = 0;
                    return;
                }
            }
        }

        if (touch.phase == TouchPhase.Ended)
        {
            interactionTime = 0;
        }
    }

    private void OnBuildingSelected(int _) 
    {
        canSelected = false;
    }

    private void OnConstructed() 
    {
        canSelected = true;
    }

    internal virtual void BuildingHold()
    {
        canSelected = false;
        GameManager.ins.GridManager.Destruct(ConstructedOnGrid.Coordinate, BuildingSize);
        OnBuildingHold?.Invoke(this);
    }

    internal virtual void BuildingClicked() 
    {
    
    }
}
