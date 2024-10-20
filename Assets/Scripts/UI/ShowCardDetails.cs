using System;
using CardBattles.CardScripts;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ShowCardDetails : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private float timeToWait = 0.5f;
    [SerializeField] private RectTransform descriptionWindow;
    [SerializeField] private TMP_Text descriptionText;
    
    private float hoverTimer = 0f;

    private Card card;
    [Obsolete]
    private CardDetail cardDetail;

    private bool isMouseOver;
    
    [Obsolete("Obsolete")]
    private void Awake() {
        card = GetComponent<Card>();
        cardDetail = GetComponent<CardDetail>();
    }
    
    private void Update() {
        if (PauseManager.Instance.IsOpen)
            return;
        if (cardDetail is not null && InventoryController.Instance.IsOpen())
            return;
        
        if (isMouseOver) {
            hoverTimer += Time.deltaTime;
            if (hoverTimer >= timeToWait) {
                MoveTextNearCursor();
                ShowDetails();
            }
        }
        else {
            hoverTimer = 0f;
            HideDetails();
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData) {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        isMouseOver = false;
    }
    
    private void MoveTextNearCursor() {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        descriptionWindow.sizeDelta = new Vector2(descriptionText.preferredWidth > 300 ?
            300 : descriptionText.preferredWidth, descriptionText.preferredHeight);
        descriptionWindow.transform.position = new Vector2(mousePosition.x + 20, mousePosition.y + descriptionWindow.sizeDelta.y/2);
    }

    private void ReadCardData() {
        //TODO UPDATE TO NEW ONES
        /*if (cardOld is not null) {
            cardData = cardOld.cardData;
        }
        else {
            cardData = cardDetail.CardDatas;
        }
        string displayText = cardData.cardName + "\nDescription: ";
        displayText += "description text";
        
        if (cardData is Minion) {
            displayText += "\nPower: " + ((MinionCardData)cardData).power;
            displayText += "\nHealth: " + ((MinionCardData)cardData).maxHealth;
        }
        

        descriptionText.text = displayText;
        descriptionWindow.gameObject.SetActive(true);
        */
    }
    
    private void ShowDetails() {
        ReadCardData();
    }
    
    private void HideDetails() {
        descriptionWindow.gameObject.SetActive(false);
    }

    public void SetComponents(RectTransform window, TMP_Text tmpText) {
        descriptionWindow = window;
        descriptionText = tmpText;
    }
}