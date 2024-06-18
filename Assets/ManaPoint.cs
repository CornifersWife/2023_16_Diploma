using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ManaPoint : MonoBehaviour {
    private Image image;

    private bool isAvailable = false;
    [SerializeField] private Color isAvailableColor = Color.cyan;
    [SerializeField] private Color isEmptyColor = Color.gray;

    private void Awake() {
        image = GetComponent<Image>();
    }

    public void LackOfMana() {
        throw new System.NotImplementedException();
    }

    //TODO MAGIC NUMBERS
    public void IsAvaliable(bool value) {
        var newColor = value ? isAvailableColor : isEmptyColor;
        image.DOColor(newColor, 0.2f).SetEase(Ease.InOutCubic);
    }
}
