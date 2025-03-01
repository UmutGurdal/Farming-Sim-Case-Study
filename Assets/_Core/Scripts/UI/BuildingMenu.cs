using UnityEngine;

public class BuildingMenu : MonoBehaviour
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
        gameObject.SetActive(false);
    }
}
