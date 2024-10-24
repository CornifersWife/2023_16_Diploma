﻿using System.Collections;
using System.Collections.Generic;
using System;
using CardBattles.CardScripts.Effects;
using UnityEngine;
using UnityEngine.EventSystems;
using CardBattles.Enums;

[Serializable]
public class TriggerEffectDictionary : SerializableDictionary<EffectTrigger, EffectTargetValue>{}
[Serializable]
public class StringStringDictionary : SerializableDictionary<string, string> {}
[Serializable]
public class IntStringDictionary : SerializableDictionary<int, string> {}
[Serializable]
public class StringFloatDictionary :  SerializableDictionary<string, float>{ }
[Serializable]
public class StringIntDictionary : SerializableDictionary<string, int> {}


[Serializable]
public class ObjectColorDictionary : SerializableDictionary<UnityEngine.Object, Color> {}

[Serializable]
public class ColorArrayStorage : SerializableDictionary.Storage<Color[]> {}

[Serializable]
public class StringColorArrayDictionary : SerializableDictionary<string, Color[], ColorArrayStorage> {}

[Serializable]
public class MyClass
{
    public int i;
    public string str;
}

[Serializable]
public class QuaternionMyClassDictionary : SerializableDictionary<Quaternion, MyClass> {}