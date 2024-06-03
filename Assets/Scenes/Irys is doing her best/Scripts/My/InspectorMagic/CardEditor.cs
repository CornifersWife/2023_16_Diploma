using UnityEditor;
using UnityEngine;
using Scenes.Irys_is_doing_her_best.Scripts.My;

[CustomEditor(typeof(CardData), true)]
public class CardEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI(); // Draws the default inspector

        serializedObject.Update();

       // DrawPropertiesExcluding(serializedObject, "onPlayEffects");
       // SerializedProperty effectsProperty = serializedObject.FindProperty("onPlayEffects");
       // EditorGUILayout.PropertyField(effectsProperty, true);

        serializedObject.ApplyModifiedProperties();
    }
}