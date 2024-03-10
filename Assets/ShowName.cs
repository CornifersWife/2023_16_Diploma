using System;
using TMPro;
using UnityEngine;

public class ShowName : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private GameObject _textObject;

    private void OnMouseOver()
    {
        Debug.Log("Active");
        _textObject = panel.transform.GetChild(0).gameObject;
        _textObject.GetComponent<TextMeshProUGUI>().text = this.gameObject.name;
        panel.SetActive(true);
    }

    private void OnMouseExit()
    {
        Debug.Log("Not active");
        if (_textObject != null)
        {
            _textObject.GetComponent<TextMeshProUGUI>().text = "";
        }
        panel.SetActive(false);
    }
}
