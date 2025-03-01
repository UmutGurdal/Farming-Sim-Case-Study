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

#if UNITY_EDITOR
    private void OnValidate()
    {
        Init();
        if (ManuallyValidate)
            Debug.Log("GameManager Validated");
        ManuallyValidate = false;
    }
#endif

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Init()
    {
        Camera = Camera.main;
        GridManager = GetComponentInChildren<GridManager>();
        Builder = GetComponentInChildren<Builder>();
    }
}
