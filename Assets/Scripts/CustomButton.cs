using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : Button
{
    private GameObject _selectedCard;
    private GameObject _hand;
    private int _selectedIndex;
    private CardDisplay _cardDisplay;

    public GameObject Process()
    {
        _selectedCard = transform.parent.parent.gameObject;
        _hand = _selectedCard.transform.parent.gameObject;
        _cardDisplay = _selectedCard.GetComponent<CardDisplay>();
        _selectedIndex = _hand.GetComponent<HandManager>().GetCardIndex(_cardDisplay.cardData);
        Debug.Log("Selected card: " + _selectedCard);
        return _selectedCard;
    }
}
