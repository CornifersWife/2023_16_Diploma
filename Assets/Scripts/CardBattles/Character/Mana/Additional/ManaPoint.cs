using System;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace CardBattles.Character.Mana.Additional {
    public class ManaPoint : MonoBehaviour {
        [SerializeField] private float changeStateScaleFlash = 1.2f;
        [SerializeField,Required]
        public Image OnImage;
        [SerializeField,Required]
        public Image OffImage;

        public IEnumerable<Image> Images => new[] { OnImage, OffImage };
        public bool isAvailable = true;

        private void Awake() {
            SetState(isAvailable);
        }

        public void SetState(bool available,float transitionTime = 0.3f) {
            if(isAvailable == available)
                return;
            isAvailable = available;
            var colorTo = available ? Color.white : Color.clear;
            OnImage.DOColor(colorTo, transitionTime);
            var growTween = transform.DOScale(Vector3.one * changeStateScaleFlash, transitionTime ).SetEase(ease: Ease.InOutSine);
            var shrinkTween = transform.DOScale(Vector3.one, transitionTime * 0.9f).SetEase(Ease.InCubic);
            var sequence = DOTween.Sequence();
            sequence.Append(growTween);
            sequence.Append(shrinkTween);
            sequence.Play();
        }
    }
}