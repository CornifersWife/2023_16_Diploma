using UnityEngine;

public class BoardSideManager : MonoBehaviour {
    public GameObject cardPrefab;
    public int count = 5;
    public CardDisplay[] board;
    public float cardSpacing = 1.0f;
    
    private void Start() {
        board = new CardDisplay[count];
    }
    public void UpdateCardPositionsOnBoard() {
        for (int i = 0; i < count; i++) {
            Vector3 cardPos = new Vector3(i * cardSpacing, 0, 0);
            print(cardPos);
            if(board[i]!=null)
                board[i].transform.localPosition = cardPos;
        }
    }
    public void AddCardToBoard(BaseCardData cardData,int index) {
        GameObject cardObj = Instantiate(cardPrefab, transform);
        CardDisplay cardDisplay = cardObj.GetComponent<CardDisplay>();
        cardDisplay.SetupCard(cardData);
        board[index] = cardDisplay;
        
        UpdateCardPositionsOnBoard();
        cardDisplay.DisplayData(cardObj);
    }
    public void RemoveCardFromBoard(int index) {
        board[index] = null;
    }
  
}
