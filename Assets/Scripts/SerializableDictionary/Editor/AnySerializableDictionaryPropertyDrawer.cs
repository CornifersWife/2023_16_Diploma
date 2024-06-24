using UnityEditor;

namespace SerializableDictionary.Editor {
    
    [CustomPropertyDrawer(typeof(StringIntDictionary))]
    [CustomPropertyDrawer(typeof(StringFloatDictionary))]
    [CustomPropertyDrawer(typeof(TriggerEffectDictionary))]
    [CustomPropertyDrawer(typeof(IntStringDictionary))]
    [CustomPropertyDrawer(typeof(StringStringDictionary))]
    public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}
}