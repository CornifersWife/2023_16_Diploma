using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManageCardSetDetails : MonoBehaviour {
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject cardListSpace;
    [SerializeField] private GameObject cardSetDetailPrefab;
    [SerializeField] private RectTransform descriptionWindow;
    [SerializeField] private TMP_Text descriptionText;

    private List<BaseCardData> cards = new List<BaseCardData>();
    private Animator animator;
    private bool isOpen;
    public bool IsOpen => isOpen;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    
    public void ReadCardSet(CardSetData cardSetData) {
        isOpen = true;
        nameText.text = cardSetData.displayName;

        foreach (BaseCardData cardData in cardSetData.cards) {
            cards.Add(cardData);
        }
        DisplayCards();
    }

    private void DisplayCards() {
        foreach (BaseCardData cardData in cards) {
            SetUpObjects(cardData);
        }
        animator.SetBool("isOpen", true);
    }

    private void SetUpObjects(BaseCardData cardData) {
        GameObject displayObject = Instantiate(cardSetDetailPrefab, cardListSpace.transform, true);
        displayObject.AddComponent<CardDetail>().CardData = cardData;
        displayObject.AddComponent<ShowCardDetails>().SetComponents(descriptionWindow, descriptionText);
        displayObject.transform.GetChild(0).GetComponent<Image>().sprite = cardData.cardImage;
        displayObject.transform.GetChild(1).GetComponent<TMP_Text>().text = cardData.cardName;
    }

    public void Hide() {
        animator.SetBool("isOpen", false);
        foreach (Transform displayObject in cardListSpace.transform) {
            Destroy(displayObject.gameObject);
        }
        cards.Clear();
        isOpen = false;
    }
}