using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[SuppressMessage("ReSharper", "Unity.InefficientPropertyAccess")]
public class HoverEffect : MonoBehaviour
{
    public Vector3 scaleIncrease = new Vector3(1.2f, 1.2f, 1.2f);
    public float liftOff = 0.1f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void OnMouseEnter()
    {
        transform.localScale = Vector3.Scale(originalScale, scaleIncrease);
        var position = transform.position;
        transform.position = new Vector3(position.x, position.y + liftOff, position.z);
    }

    void OnMouseExit()
    {
        transform.localScale = originalScale;
        var position = transform.position;
        transform.position = new Vector3(position.x, position.y- liftOff, position.z);
    }
}