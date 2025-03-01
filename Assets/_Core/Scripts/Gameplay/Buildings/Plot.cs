using System;
using UnityEngine;

public class Plot : Building
{
    public static event Action<Plot> OnPlotSelect;

    [SerializeField] private Crop[] crops;

    internal override void OnEnable()
    {
        base.OnEnable();
        OnPlotSelect += OnPlotSelected;
        CropSelector.OnCropSelected += OnCropSelected;
    }

    internal override void OnDisable()
    {
        base.OnEnable();
        OnPlotSelect -= OnPlotSelected;
        CropSelector.OnCropSelected -= OnCropSelected;
    }

    internal override void BuildingClicked()
    {
        base.BuildingClicked();

        if (plantedCrop && plantedCrop.TryHarvest())
        {
            plantedCrop = null;
            return;
        }

        UIManager.ins.CropMenu.ShowMenu();
        OnPlotSelect?.Invoke(this);
    }

    bool isSelected;
    private void OnPlotSelected(Plot selectedPlot) 
    {
        isSelected = true;

        if (selectedPlot != this)
            isSelected = false;
    }

    Crop plantedCrop;
    private void OnCropSelected(int id) 
    {
        if (plantedCrop != null)
            return;

        if (isSelected == false)
            return;

        plantedCrop = Instantiate(crops[id], transform.position, Quaternion.identity, transform);
    }
}
