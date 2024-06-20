using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ManaPoint : MonoBehaviour {
    public Image image;


    private bool isAvailable = true;
    [SerializeField] private Color isAvailableColor = Color.cyan;
    [SerializeField] private Color isEmptyColor = Color.gray;


    [BoxGroup("Refresh Animation"), SerializeField, Range(0.5f, 2f)]
    private float refreshScapeUp = 2f;

    [BoxGroup("Refresh Animation"), SerializeField]
    private float refreshAnimationDuration = 0.8f;

    [FormerlySerializedAs("ease")] [BoxGroup("Refresh Animation"), SerializeField]
    private Ease animationEase = Ease.InCubic;

    private void Awake() {
        image = GetComponent<Image>();
        image.color = isAvailable ? isAvailableColor : isEmptyColor;
    }


    //TODO MAGIC NUMBERS 
    //TODO HATE THIS ANIMATION SHOULD BE SNAPPIER
    public void IsAvailable(bool value) {
        if (value && !isAvailable) {
            var colorChange = DOTween.Sequence().SetEase(animationEase);
            image.color = Color.white;
            colorChange.Join(image.DOColor(isAvailableColor, refreshAnimationDuration / 2f));
           
            var scaleChange = DOTween.Sequence().SetEase(animationEase);
            scaleChange.Join(transform.DOScale(Vector3.one * refreshScapeUp, refreshAnimationDuration / 4f));
            scaleChange.Append(transform.DOScale(Vector3.one, refreshAnimationDuration /2f));

            scaleChange.Play();
            colorChange.Play();
        }
        else {
            var newColor = value ? isAvailableColor : isEmptyColor;
            image.DOColor(newColor, 0.2f).SetEase(Ease.InOutCubic);
        }

        isAvailable = value;
    }

    public void LackOfMana() {
    }
}