using System.Collections;
using System.Collections.Generic;
using CardBattles.ForEditor;
using NaughtyAttributes;
using UnityEngine;

public class TimeScaler : ForEditorComponent {
    [SerializeField] [OnValueChanged("UpdateTimeCallback")] [Range(0, 1)]
    private float timeScale = 1f;

    public void UpdateTimeCallback() {
        if(!enabled)
            return;
        Time.timeScale = timeScale;
    }

}
