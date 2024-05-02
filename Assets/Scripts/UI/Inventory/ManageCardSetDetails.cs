using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManageCardSetDetails : MonoBehaviour {
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject cardListSpace;
    [SerializeField] private GameObject cardSetDetailPrefab;

    private List<BaseCardData> cards = new List<BaseCardData>();
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    
    public void ReadCardSet(CardSetData cardSetData) {
        nameText.text = cardSetData.displayName;

        foreach (BaseCardData cardData in cardSetData.cards) {
            cards.Add(cardData);
        }
        DisplayCards();
    }

    private void DisplayCards() {
        foreach (BaseCardData cardData in cards) {
            SetUpObjects(cardData.cardImage, cardData.cardName);
        }
        animator.SetBool("isOpen", true);
    }

    private void SetUpObjects(Sprite sprite, string text) {
        GameObject displayObject = Instantiate(cardSetDetailPrefab, cardListSpace.transform, true); 
        displayObject.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        displayObject.transform.GetChild(1).GetComponent<TMP_Text>().text = text;
    }

    public void Hide() {
        animator.SetBool("isOpen", false);
    }
}