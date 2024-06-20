using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;


//TODO hate this script, someone please rework it, its awful, thankfully this isn't important code but :(
public class ManaDisplay : MonoBehaviour {
    [SerializeField] public GameObject manaPointPrefab;

    [SerializeField] private float distanceBetweenManaPoints = 150f;
    [SerializeField] private List<ManaPoint> manaPoints;

    [SerializeField] private Vector3 startingPositionOffset;

    [BoxGroup("No mana animation")] [Label("Time")] [SerializeField]
    private float shakeTime = 0.3f;

    [BoxGroup("No mana animation")] [Label("Ease")] [SerializeField]
    private Ease noManaAnimationEase;

    [BoxGroup("No mana animation")] [SerializeField]
    private int shakeStrength = 15;

    //TODO read up on how to use it
    private int currentMana; // backing field

    public int CurrentMana {
        get => currentMana;
        set {
            if (currentMana != value) {
                currentMana = value;
                OnCurrentManaChanged(value);
            }
        }
    }


    public void UpdateMaxMana(int value) {
        SetManaPointAmount(value);

        //TODO NOT SURE IF THIS IS NEEDED
        OnCurrentManaChanged(currentMana);
    }

    private void SetManaPointAmount(int value) {
        if (value == manaPoints.Count)
            return;
        var firstPosition = transform.position + startingPositionOffset;
        var positions = new List<Vector3>();
        for (int i = 0; i < value; i++) {
            var pos = firstPosition;
            pos.x += distanceBetweenManaPoints * i;
            positions.Add(pos);
        }

        //TODO it seems so wrong to do it this way, dont have brain energy to do it smarter
        manaPoints.ForEach(Destroy);
        manaPoints = new List<ManaPoint>();
        //

        foreach (var pos in positions) {
            var manaPoint = Instantiate(manaPointPrefab, transform);
            manaPoints.Add(manaPoint.GetComponent<ManaPoint>());
            manaPoint.transform.position = pos;
        }
    }

    private void OnCurrentManaChanged(int value) {
        if (value > manaPoints.Count) {
            Debug.LogError("shouldn't reach this code, if it does, fix the code");
        }

        for (int i = 0; i < manaPoints.Count; i++) {
            manaPoints[i].IsAvailable(i < value);
        }
    }

    public void ShowLackOfMana() {
        var xd = DOTween.Sequence();
        xd.Append(transform.DOShakePosition(shakeTime, new Vector3(shakeStrength, 0, 0),
            randomnessMode: ShakeRandomnessMode.Harmonic).SetLoops(3,LoopType.Yoyo));
        foreach (var manaPoint in manaPoints) {
            xd.Join(manaPoint.image.DOColor(Color.red, shakeTime).SetEase(noManaAnimationEase)
                .SetLoops(4, LoopType.Yoyo));
        }

        xd.Play();
    }
}