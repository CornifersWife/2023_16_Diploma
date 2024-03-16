using UnityEngine;

public class SceneTriggerPopup : MonoBehaviour
{
    [SerializeField] private string loadName;
    [SerializeField] private string unloadName;
    [SerializeField] private RectTransform popupPanel;

    private void Awake()
    {
        popupPanel.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        popupPanel.gameObject.SetActive(true);
    }

    public void YesClicked()
    {
        if (loadName != "")
            SceneSwitcher.Instance.LoadScene(loadName);
        if(unloadName != "")
            SceneSwitcher.Instance.UnloadScene(unloadName);
    }

    public void NoClicked()
    {
        popupPanel.gameObject.SetActive(false);
    }
}