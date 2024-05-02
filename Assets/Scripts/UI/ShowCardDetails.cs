using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowCardDetails : MonoBehaviour {
    [SerializeField] private float timeToWait = 0.5f;
    [SerializeField] private RectTransform descriptionWindow;
    [SerializeField] private TMP_Text descriptionText;
    
    private Camera mainCamera;
    private float hoverTimer = 0f;

    private CardDisplay cardDisplay;
    private CardDetail cardDetail;
    
    private void Awake() {
        mainCamera = Camera.main;
        cardDisplay = GetComponent<CardDisplay>();
        cardDetail = GetComponent<CardDetail>();
        
        Debug.Log(gameObject);
    }
    
    private void Update() {
        if (IsMouseOver()) {
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
    
    private bool IsMouseOver() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit) && hit.collider.gameObject == gameObject) {
            Debug.Log("Hover");
            return true;
        }
        return false;
    }
    
    private void MoveTextNearCursor() {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        descriptionWindow.sizeDelta = new Vector2(descriptionText.preferredWidth > 300 ?
            300 : descriptionText.preferredWidth, descriptionText.preferredHeight);
        descriptionWindow.transform.position = new Vector2(mousePosition.x + 20, mousePosition.y + descriptionWindow.sizeDelta.y/2);
    }

    private void ReadCardData() {
        BaseCardData cardData;
        if (cardDisplay is not null) {
            cardData = cardDisplay.cardData;
        }
        else {
            cardData = cardDetail.CardData;
        }
        string displayText = cardData.cardName + "\nDescription: ";
        displayText += "description text";
        
        if (cardData is MinionCardData) {
            displayText += "\nPower: " + ((MinionCardData)cardData).power;
            displayText += "\nHealth: " + ((MinionCardData)cardData).maxHealth;
        }

        descriptionText.text = displayText;
        descriptionWindow.gameObject.SetActive(true);
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