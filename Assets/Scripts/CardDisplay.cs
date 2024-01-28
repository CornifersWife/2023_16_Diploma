using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {
    public BaseCardData cardData;
    
    public void SetupCard(BaseCardData data) {
        cardData = data;
        DisplayData(gameObject);
    }
    
    public void SetupCard(MinionCardData minionData) {
        cardData = minionData;
        DisplayData(gameObject);
    }

    public void DisplayData(GameObject card)
    {
        GameObject canvas = card.transform.GetChild(0).gameObject;
        GameObject cardText = canvas.transform.GetChild(0).gameObject;

        GameObject idText = cardText.transform.GetChild(0).gameObject;
        GameObject nameText = cardText.transform.GetChild(1).gameObject;
        GameObject manaText = cardText.transform.GetChild(2).gameObject;
        GameObject attackText = cardText.transform.GetChild(3).gameObject;
        GameObject healthText = cardText.transform.GetChild(4).gameObject;

        GameObject Image = canvas.transform.GetChild(1).gameObject;
        
        idText.GetComponent<TextMeshProUGUI>().text = cardData.id.ToString();
        nameText.GetComponent<TextMeshProUGUI>().text = cardData.cardName;
        manaText.GetComponent<TextMeshProUGUI>().text = cardData.cost.ToString();
        Image.GetComponent<Image>().sprite = cardData.cardImage;
        
        //If card is Minion:
        MinionCardData minionCardData = (MinionCardData)cardData;
        if (cardData is MinionCardData)
        {
            attackText.GetComponent<TextMeshProUGUI>().text = minionCardData.power.ToString();
            healthText.GetComponent<TextMeshProUGUI>().text = minionCardData.currentHealth.ToString();
        }

        //If card is not Minion:
        else
        {
            attackText.GetComponent<TextMeshProUGUI>().text = "";
            healthText.GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
