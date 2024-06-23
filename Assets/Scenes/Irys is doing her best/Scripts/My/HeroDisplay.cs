using System;
using System.Collections;
using DG.Tweening;
using NaughtyAttributes;
using Scenes.Irys_is_doing_her_best.Scripts.My.Board;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scenes.Irys_is_doing_her_best.Scripts.My {
    public class HeroDisplay : MonoBehaviour {
        [Foldout("Objects"), SerializeField] private Image heroImage;
        [Foldout("Objects"), SerializeField] private TextMeshProUGUI hpText;

        [Foldout("Text"), SerializeField] private Color fullHpColor = Color.black;
        [Foldout("Text"), SerializeField] private Color missingHpColor = new Color(90, 30, 30);


        [Foldout("Hit Animation"), SerializeField]
        private float hitAnimatonDuration;
        [Foldout("Hit Animation"), SerializeField]
        private float hitAnimatonShakeStrength;
        [Foldout("Hit Animation"), SerializeField]
        private float hitAnimatonKnockbackStrength;
        
        private Hero hero;

        private int currentHealth;

        private void Awake() {
            hero = GetComponent<Hero>();
            hero.currentHealthAction += SetCurrentHealth;
            hero.takeDamageAction += GetHit;
        }

        private void SetCurrentHealth(int value) {
            SetHpColor();
            hpText.text = value.ToString();
        }
        
        private void SetHpColor() {
            if (hero.HasFullHp)
                hpText.color = fullHpColor;
            else {
                hpText.color = missingHpColor;
            }
        }

        private void GetHit() {
            StartCoroutine(GetHitAnimation());
        }
        private IEnumerator GetHitAnimation() {
            yield return null;
            /*var shake = heroImage.transform
                .DOShakePosition(hitAnimatonDuration,hitAnimatonShakeStrength);
            var knockback = heroImage.transform
                .DOMove(,hitAnimatonShakeStrength);
            shake.Play();*/
        }
        
        
    }
}