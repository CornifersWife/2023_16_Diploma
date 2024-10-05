using UnityEngine;
using UnityEngine.UI;

public class ShowQuestIndicator : MonoBehaviour {
    [SerializeField] private GameObject questImage;

    public void ShowQuestIcon() {
        questImage.SetActive(true);
    }
    
    public void HideQuestIcon() {
        questImage.SetActive(false);
    }
}
