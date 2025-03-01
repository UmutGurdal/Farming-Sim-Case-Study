using Muchwood.Utils;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
#if UNITY_EDITOR
    [SerializeField] private bool ManuallyValidate;
#endif
    [ReadOnly] public Camera Camera;
    [ReadOnly] public GridManager GridManager;
    [ReadOnly] public Builder Builder;

    private void OnValidate()
    {
        Init();
        if (ManuallyValidate)
            Debug.Log("GameManager Validated");
        ManuallyValidate = false;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Camera = Camera.main;
        GridManager = GetComponentInChildren<GridManager>();
        Builder = GetComponentInChildren<Builder>();
    }
}
