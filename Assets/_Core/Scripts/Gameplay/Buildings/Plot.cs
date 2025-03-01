using UnityEngine;

public class Plot : Building
{
    

    internal override void BuildingClicked()
    {
        base.BuildingClicked();
        UIManager.ins.CropMenu.ShowMenu();
    }
}
