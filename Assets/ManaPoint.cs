using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ManaPoint : MonoBehaviour {
    public Image image;
    public bool isAvailable = true;
    private void Awake() {
        image = GetComponent<Image>();
    }
}