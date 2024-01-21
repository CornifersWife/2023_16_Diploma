using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardDisplay : MonoBehaviour {
    public BaseCardData cardData;
    public MinionCardData minionCardData;
    public GameObject cardCanvas;
    
    public void SetupCard(BaseCardData data) {
        cardData = data;
        // Update the card's visuals here
        // For example, set the card's sprite to cardData.cardImage
    }
    
    public void SetupMinionCard(MinionCardData minionData) {
        minionCardData = minionData;
    }

    public void DisplayData(GameObject card)
    {
        GameObject cardCanvas = Instantiate(this.cardCanvas, card.transform.position, transform.rotation * Quaternion.Euler (90f, 0f, 180f));
        cardCanvas.transform.SetParent(card.transform);
        GameObject cardText = cardCanvas.transform.GetChild(0).gameObject;

        GameObject idText = cardText.transform.GetChild(0).gameObject;
        GameObject nameText = cardText.transform.GetChild(1).gameObject;
        GameObject manaText = cardText.transform.GetChild(2).gameObject;
        GameObject attackText = cardText.transform.GetChild(3).gameObject;
        GameObject healthText = cardText.transform.GetChild(4).gameObject;
        
        idText.GetComponent<TextMeshProUGUI>().text = cardData.id.ToString();
        nameText.GetComponent<TextMeshProUGUI>().text = cardData.cardName;
        manaText.GetComponent<TextMeshProUGUI>().text = cardData.cost.ToString();
        
        //If card is Minion:
        /*
        attackText.GetComponent<TextMeshProUGUI>().text = cardData.power.ToString();
        healthText.GetComponent<TextMeshProUGUI>().text = cardData.health.ToString();
        */
    }
}
