using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace Editor.tmp {
    [CustomEditor(typeof(AudioCollection))]
    public class AudioLoaderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // Draws the default properties (like the JSON input)
            AudioCollection audioCollection = (AudioCollection)target;

            if (audioCollection.audioMap != null && audioCollection.audioMap.Count > 0)
            {
                EditorGUILayout.LabelField("Audio Map", EditorStyles.boldLabel);
                foreach (var entry in audioCollection.audioMap)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(entry.key, GUILayout.MaxWidth(100));
                    entry.clip = (AudioClip)EditorGUILayout.ObjectField(entry.clip, typeof(AudioClip), false);
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }
}