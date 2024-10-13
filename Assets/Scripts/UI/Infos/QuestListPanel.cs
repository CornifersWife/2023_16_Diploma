using DG.Tweening;
using Events;
using QuestSystem;
using TMPro;
using UnityEngine;

public class QuestListPanel : MonoBehaviour {
    [Header("Time")] 
    [SerializeField] private float fadeTime = 1f;

    [Header("Setup")] 
    [SerializeField] private RectTransform questPanel;
    [SerializeField] private GameObject questList;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private GameObject questInfoPrefab;
    
    private void OnEnable() {
        GameEventsManager.Instance.QuestEvents.OnStartQuest += AddQuestToList;
    }
    
    private void OnDisable() {
        GameEventsManager.Instance.QuestEvents.OnStartQuest -= AddQuestToList;
    }

    private void AddQuestToList(string questId) {
        QuestInfoSO questInfo = questManager.GetQuestById(questId).info;
        GameObject displayObject = Instantiate(questInfoPrefab, questList.transform, true);
        displayObject.transform.GetChild(0).GetComponent<TMP_Text>().text = questInfo.displayName;
        displayObject.transform.GetChild(1).GetComponent<TMP_Text>().text = questInfo.questDescription;
    }
    
    public void PanelFadeIn() {
        questPanel.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.InOutQuint);
    }

    public void PanelFadeOut() {
        questPanel.DOAnchorPos(new Vector2(500f, 0f), fadeTime, false).SetEase(Ease.InOutQuint);
    }
}
