using UnityEngine;

public class CardMovement : MonoBehaviour
{
    public void TransformToSpot(Transform transform)
    {
        if (transform != null)
        {
            transform.SetParent(transform, false);
        }
        else
        {
            Debug.LogError("Board Transform is not set.", this);
        }
    }
}