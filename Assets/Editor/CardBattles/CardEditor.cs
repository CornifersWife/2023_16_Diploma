using System;
using CardBattles.CardScripts.CardDatas;
using UnityEditor;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.InspectorMagic {
    [CustomEditor(typeof(CardData), true)]
    public class CardDataEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if (GUI.changed) {
                EditorUtility.SetDirty(target);
                var nameGiven = ((CardData)target).cardName;
                if (String.IsNullOrWhiteSpace(nameGiven))
                    nameGiven = "No name given";
                ((CardData)target).name = nameGiven;
                AssetDatabase.SaveAssets();
            }
        }
    }
}