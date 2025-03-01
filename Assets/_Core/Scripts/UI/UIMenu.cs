using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private RectTransform panel;

    public void ShowMenu() 
    {
        panel.gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        panel.gameObject.SetActive(false);
    }

}
