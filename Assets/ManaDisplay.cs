using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class ManaDisplay : MonoBehaviour {
    [SerializeField] public GameObject manaPointPrefab;
    [SerializeField] public List<ManaPoint> manaPoints;

    //TODO read up on how to use it
    [OnValueChanged("xd")]
    public int currentMana;
    private void Awake() {
        manaPoints =gameObject.GetComponentsInChildren<ManaPoint>().ToList();
    }

    
    
    public void ShowLackOfMana() {
        foreach (var manaPoint in manaPoints) {
            manaPoint.LackOfMana();
        }
    }
}
