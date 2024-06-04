using UnityEditor;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.InspectorMagic {
    [CustomPropertyDrawer(typeof(EffectTargetPair))]
    public class EffectTargetPairDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            var effectProperty = property.FindPropertyRelative("effect");
            var targetTypeProperty = property.FindPropertyRelative("targetType");
            
            // Calculate rects
            var effectRect = new Rect(position.x, position.y, position.width / 2 - 10, position.height);
            var targetTypeRect = new Rect(position.x + position.width / 2 + 10, position.y, position.width / 2 - 10, position.height);

            // Draw fields
            EditorGUI.PropertyField(effectRect, effectProperty, GUIContent.none);
            EditorGUI.PropertyField(targetTypeRect, targetTypeProperty, GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}