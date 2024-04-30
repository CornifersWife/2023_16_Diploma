using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Short Dialogue")]
public class ShortDialogue : Dialogue {
    [SerializeField] private float showForSeconds = 2;

    public float Seconds => showForSeconds;
}