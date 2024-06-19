using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ManaPoint : MonoBehaviour {
    public Image image;


    private bool isAvailable = true;
    [SerializeField] private Color isAvailableColor = Color.cyan;
    [SerializeField] private Color isEmptyColor = Color.gray;


    private void Awake() {
        image = GetComponent<Image>();
        image.color = isAvailable ? isAvailableColor : isEmptyColor;
    }


    //TODO MAGIC NUMBERS
    public void IsAvaliable(bool value) {
        var newColor = value ? isAvailableColor : isEmptyColor;
        image.DOColor(newColor, 0.2f).SetEase(Ease.InOutCubic);
    }

    public void LackOfMana() {
        
    }
}