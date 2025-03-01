using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelector : MonoBehaviour
{
    public static event Action<int> OnBuildingSelected;

    public int BuildingID;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnSelectBuilding);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnSelectBuilding);
    }

    private void OnSelectBuilding() 
    {
        OnBuildingSelected?.Invoke(BuildingID);
    }
}
