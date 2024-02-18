using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {
    public BaseCardData cardData;
    private String _healthText;
    
    public float attackAnimatonDuration = 1.0f; // Duration of the move towards the target and back
    
    
    public void SetupCard(BaseCardData data) {
        cardData = data;
        DisplayData(gameObject);
    }
    
    public void SetupCard(MinionCardData minionData) {
        cardData = minionData;
        minionData.currentHealth = minionData.maxHealth;
        minionData.OnHealthChanged += UpdateHealthDisplay; // Correctly subscribe to the event
        minionData.OnAttack += AttackTarget;
        DisplayData(gameObject);
    }

    public void AttackTarget(Vector3 targetPosition) {
        StartCoroutine(MoveTowardsTarget(targetPosition));
    }
    IEnumerator MoveTowardsTarget(Vector3 targetPosition)
    {
        float elapsedTime = 0.0f;
        Vector3 startPosition = transform.position;

        // Ease out (start slow, then accelerate)
        while (elapsedTime < attackAnimatonDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / (attackAnimatonDuration / 2);
            t = Mathf.Sin(t * Mathf.PI * 0.5f); // Using Sin for ease out
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        // Reset elapsedTime for the return journey
        elapsedTime = 0.0f;

        // Ease in (start fast, then decelerate)
        while (elapsedTime < attackAnimatonDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / (attackAnimatonDuration / 2);
            t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f); // Using Cos for ease in
            transform.position = Vector3.Lerp(targetPosition, startPosition, t);
            yield return null;
        }

        // Ensure the card returns to exactly its start position
        transform.position = startPosition;
    }


    
    private void Update() {
        
        if (cardData is MinionCardData)
        {
            MinionCardData minionCardData = (MinionCardData)cardData;
            string newHealthText = minionCardData.currentHealth.ToString();

            // Check if the health text has changed before updating the UI to avoid unnecessary updates
            if (_healthText != newHealthText)
            {
                _healthText = newHealthText;
                // Find and update the health text UI component directly
                UpdateHealthDisplay(minionCardData.currentHealth);
            }
        }
    }

// This method updates the health display on the UI
    private void UpdateHealthDisplay(int newHealth) {
        string healthText = newHealth.ToString();
        GameObject canvas = gameObject.transform.GetChild(0).gameObject;
        GameObject cardText = canvas.transform.GetChild(0).gameObject;
        GameObject healthTextGameObject = cardText.transform.GetChild(3).gameObject; // Assuming health text is at child index 3
        healthTextGameObject.GetComponent<TextMeshProUGUI>().text = healthText;
        _healthText = healthText; // Update the cached health text
    }
    private void OnDestroy() {
        if (cardData is MinionCardData minionData) {
            minionData.OnHealthChanged -= UpdateHealthDisplay; // Unsubscribe to avoid memory leaks
        }
    }


    public void DisplayData(GameObject card)
    {
        GameObject canvas = card.transform.GetChild(0).gameObject;
        GameObject cardText = canvas.transform.GetChild(0).gameObject;
        
        GameObject nameText = cardText.transform.GetChild(0).gameObject;
        GameObject manaText = cardText.transform.GetChild(1).gameObject;
        GameObject attackText = cardText.transform.GetChild(2).gameObject;
        GameObject healthText = cardText.transform.GetChild(3).gameObject;
        
        //GameObject Image = canvas.transform.GetChild(1).gameObject;
        
        nameText.GetComponent<TextMeshProUGUI>().text = cardData.cardName;
        manaText.GetComponent<TextMeshProUGUI>().text = cardData.cost.ToString();
        //Image.GetComponent<Image>().sprite = cardData.cardImage;
        
        //If card is Minion:
        MinionCardData minionCardData = (MinionCardData)cardData;
        if (cardData is MinionCardData)
        {
            attackText.GetComponent<TextMeshProUGUI>().text = minionCardData.power.ToString();
            healthText.GetComponent<TextMeshProUGUI>().text = minionCardData.currentHealth.ToString();
            _healthText = healthText.GetComponent<TextMeshProUGUI>().text;
        }

        //If card is not Minion:
        else
        {
            attackText.GetComponent<TextMeshProUGUI>().text = "";
            healthText.GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
