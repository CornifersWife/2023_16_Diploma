using UnityEngine;

public class CardMovement : MonoBehaviour
{
    public void TransformToSpot(Transform transform)
    {
        Debug.LogError("it worked", this);
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