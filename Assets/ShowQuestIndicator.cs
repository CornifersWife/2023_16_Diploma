using UnityEngine;

public class ShowQuestIndicator : MonoBehaviour {
    [SerializeField] private GameObject questImage;

    public void ShowQuestIcon() {
        questImage.SetActive(true);
    }
    
    public void HideQuestIcon() {
        questImage.SetActive(false);
    }
}
