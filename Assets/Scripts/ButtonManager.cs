using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public ToggleGroup boardToggleGroup;
    public TMP_Dropdown dropdown;
    public HandManager playerHand;
    
    private Toggle _selectedToggle;

    private List<BaseCardData> cards = new List<BaseCardData>();
    private int _cardIndex;
    private int _spotIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateCardOptions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCardOptions()
    {
        cards.Clear();
        foreach(BaseCardData cardData in playerHand.hand)
        {
            cards.Add(cardData);
        }
        dropdown.ClearOptions();

        List<string> options = new List<string>();
        
        for (int i = 0; i < cards.Count; i++)
        {
            string option = i + " ID: " + cards[i].id + " " + cards[i].cardName;
            options.Add(option);
        }
        dropdown.AddOptions(options);
        dropdown.RefreshShownValue();
        _cardIndex = 0;
    }
    
    public void SelectCard(int cardIndex)
    {
        _cardIndex = cardIndex;
        //Debug.Log(_selectedCard);
        //Debug.Log(_cardIndex);
    }

    public void SelectSpot(bool isOn)
    {
        if (isOn)
        {
            _selectedToggle = boardToggleGroup.ActiveToggles().FirstOrDefault();
            if (_selectedToggle != null)
            {
                _spotIndex = _selectedToggle.GetComponent<CustomToggle>().index;
                //Debug.Log(_spotIndex);
            }
            else
            {
                Debug.Log("No spot selected!");
            }
        }
    }

    public int GetCardIndex()
    {
        return _cardIndex;
    }

    public int GetSpotIndex()
    {
        return _spotIndex;
    }

    public Toggle GetToggle()
    {
        return _selectedToggle;
    }
}
