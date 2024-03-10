using System;
using TMPro;
using UnityEngine;

public class ShowNameManager : MonoBehaviour
{
    [SerializeField] private RectTransform nameWindow;
    [SerializeField] private TextMeshProUGUI nameText;

    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    void Start()
    {
        HideName();
    }

    private void OnEnable()
    {
        OnMouseHover += ShowName;
        OnMouseLoseFocus += HideName;
    }

    private void OnDisable()
    {
        OnMouseHover -= ShowName;
        OnMouseLoseFocus -= HideName;
    }

    private void ShowName(string nameToDisplay, Vector2 mousePosition)
    {
        nameText.text = nameToDisplay;
        nameWindow.sizeDelta = new Vector2(nameText.preferredWidth > 100 ? 100 : nameText.preferredWidth, nameText.preferredHeight);
        nameWindow.gameObject.SetActive(true);
        nameWindow.transform.position = new Vector2(mousePosition.x + nameWindow.sizeDelta.x/2, mousePosition.y);
    }

    private void HideName()
    {
        nameText.text = default;
        nameWindow.gameObject.SetActive(false);
    }
}
