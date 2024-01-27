using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayCardVisualManager : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    
    private GameObject _selectedCard;
    private GameObject _hand;
    private int _selectedIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectCard()
    {
        GameObject cardButton = EventSystem.current.currentSelectedGameObject;
        Debug.Log("Button: " + cardButton);
        GameObject card = cardButton.transform.parent.gameObject.transform.parent.gameObject;
        _hand = card.transform.parent.gameObject;
        _selectedIndex = _hand.GetComponent<HandManager>().GetCardIndex(card.GetComponent<CardDisplay>().cardData);
        _selectedCard = card;
        Debug.Log("Selected card from button: " + _selectedCard);
    }

    public void SelectSpot()
    {
        Toggle selectedSpot = toggleGroup.ActiveToggles().FirstOrDefault();
        Debug.Log("Toggle: " + selectedSpot);
        Debug.Log("Selected card for toggle: " + _selectedCard);
        if (_selectedCard == null)
        {
            Debug.Log("SELECT CARD!");
        }
        else
        {
            _selectedCard.transform.position = selectedSpot.transform.position;
            _hand.GetComponent<HandManager>().RemoveCardFromHand(_selectedCard.GetComponent<CardDisplay>().cardData);
        }
        
    }
}
