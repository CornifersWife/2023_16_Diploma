using Scenes.Irys_is_doing_her_best.Scripts.My.Structure;
using Unity.VisualScripting;
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
            var halfWidth = position.width / 2 - 5;
            var effectRect = new Rect(position.x, position.y, halfWidth, EditorGUIUtility.singleLineHeight);
            var targetTypeRect = new Rect(position.x + halfWidth + 10, position.y, halfWidth, EditorGUIUtility.singleLineHeight);

            // Draw fields
            EditorGUI.PropertyField(effectRect, effectProperty, GUIContent.none,true);
            EditorGUI.PropertyField(targetTypeRect, targetTypeProperty, GUIContent.none,true);

            
            // Check if effect is a ChangeValueByEffect
            if (effectProperty.objectReferenceValue != null && effectProperty.objectReferenceValue is ChangeValueByEffect) {
                SerializedObject effectSerializedObject = new SerializedObject(effectProperty.objectReferenceValue);
                SerializedProperty amountProperty = effectSerializedObject.FindProperty("amount");

                var amountRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(amountRect, amountProperty, new GUIContent("Amount"));

                effectSerializedObject.ApplyModifiedProperties();
            }

            EditorGUI.EndProperty();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            // Check if effect is a ChangeValueByEffect
            var effectProperty = property.FindPropertyRelative("effect");
            if (effectProperty.objectReferenceValue != null && effectProperty.objectReferenceValue is ChangeValueByEffect) {
                // Add extra height for the amount field
                return base.GetPropertyHeight(property, label) + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            return base.GetPropertyHeight(property, label);
        }
    }
}
