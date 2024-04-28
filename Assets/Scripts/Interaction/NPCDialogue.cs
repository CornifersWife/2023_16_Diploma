using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour {
    [SerializeField] private List<Dialogue> dialogues;
    private int index = 0;

    public List<Dialogue> GetDialogues() {
        return dialogues;
    }

    public int GetIndex() {
        return index;
    }

    public void SetIndex(int value) {
        index = value;
    }
}
