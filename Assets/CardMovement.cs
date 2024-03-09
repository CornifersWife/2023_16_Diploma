using UnityEngine;

public class CardMovement : MonoBehaviour
{
    // Drag and drop the Board GameObject in the inspector
    public Transform boardTransform;

    // Method to call to move this card to the board
    public void MoveToBoard()
    {
        if (boardTransform != null)
        {
            // This sets the card's parent to the board, effectively moving it in the hierarchy
            transform.SetParent(boardTransform, false);
        }
        else
        {
            Debug.LogError("Board Transform is not set.", this);
        }
    }
}