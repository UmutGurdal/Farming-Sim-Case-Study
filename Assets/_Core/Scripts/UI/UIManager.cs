using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
#if UNITY_EDITOR
    [SerializeField] private bool ManuallyValidate;
#endif

    public BuildingMenu BuildingMenu;
    public CropMenu CropMenu;

    private void OnValidate()
    {
        Init();
        if (ManuallyValidate)
            Debug.Log("UIManager Validated");
        ManuallyValidate = false;
    }
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        BuildingMenu = GetComponentInChildren<BuildingMenu>();
        CropMenu = GetComponentInChildren<CropMenu>();
    }
}
