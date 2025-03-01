using System;
using UnityEngine;
using UnityEngine.UI;

public class CropSelector : MonoBehaviour
{
    public static event Action<int> OnCropSelected;

    public int CropID;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnSelectCrop);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnSelectCrop);
    }

    private void OnSelectCrop()
    {
        UIManager.ins.CropMenu.HideMenu();
        OnCropSelected?.Invoke(CropID);
    }
}
