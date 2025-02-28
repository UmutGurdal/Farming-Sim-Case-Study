using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class Builder : MonoBehaviour
{
    private Building SelectedBuilding;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
    }

    private void FixedUpdate()
    {
        if (SelectedBuilding == null)
            return;

        if (Input.touchCount <= 0)
            return;

        var touch = Input.GetTouch(0);

        var ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {
            if (hit.transform.TryGetComponent<Grid>(out Grid grid))
            {
                if (GameManager.ins.GridManager.CanBuild(grid.Coordinate, SelectedBuilding.BuildingSize) == false)
                    return;
            }
        }
    }
}
