using UnityEngine;

public class BuildingMenu : UIMenu
{
    private void OnEnable()
    {
        GridManager.OnConstructed += OnConstructed;
    }

    private void OnDisable()
    {
        GridManager.OnConstructed -= OnConstructed;
    }

    private void OnConstructed() 
    {
        HideMenu();
    }
}
