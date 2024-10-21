using System.Collections.Generic;
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

    private Dictionary<string, GameObject> listOfQuests;

    private void Awake() {
        listOfQuests = new Dictionary<string, GameObject>();
    }
    
    private void OnEnable() {
        GameEventsManager.Instance.QuestEvents.OnStartQuest += AddQuestToList;
        GameEventsManager.Instance.QuestEvents.OnFinishQuest += RemoveQuest;
    }
    
    private void OnDisable() {
        GameEventsManager.Instance.QuestEvents.OnStartQuest -= AddQuestToList;
        GameEventsManager.Instance.QuestEvents.OnFinishQuest -= RemoveQuest;
    }

    private void AddQuestToList(string questId) {
        QuestInfoSO questInfo = questManager.GetQuestById(questId).info;
        GameObject displayObject = Instantiate(questInfoPrefab, questList.transform, true);
        displayObject.transform.GetChild(0).GetComponent<TMP_Text>().text = questInfo.displayName;
        displayObject.transform.GetChild(1).GetComponent<TMP_Text>().text = questInfo.questDescription;
        listOfQuests.TryAdd(questId, displayObject);
    }

    private void RemoveQuest(string questId) {
        Destroy(listOfQuests[questId]);
        listOfQuests.Remove(questId);
    }
    
    public void PanelFadeIn() {
        questPanel.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.InOutQuint);
    }

    public void PanelFadeOut() {
        questPanel.DOAnchorPos(new Vector2(500f, 0f), fadeTime, false).SetEase(Ease.InOutQuint);
    }
}
