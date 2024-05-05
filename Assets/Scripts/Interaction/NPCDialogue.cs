using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour {
    [SerializeField] private List<MainDialogue> mainDialogues;
    private int mainIndex;
    
    [SerializeField] private List<ShortDialogue> shortDialogues;
    private int shortIndex;

    public List<MainDialogue> GetMainDialogues() {
        return mainDialogues;
    }

    public int GetMainIndex() {
        return mainIndex;
    }

    public void SetMainIndex(int value) {
        mainIndex = value;
    }
    
    public ShortDialogue GetShortDialogue() {
        if (shortDialogues.Count == 0 || shortIndex >= shortDialogues.Count)
            return null;
        return shortDialogues[shortIndex];
    }

    public int GetShortIndex() {
        return shortIndex;
    }

    public void SetShortIndex(int value) {
        shortIndex = value;
    }
}
