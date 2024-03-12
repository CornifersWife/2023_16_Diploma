using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {
    public BaseCardData cardData;
    private String _healthText;
    
    [SerializeField] private float attackAnimationDuration = 0.2f; // Duration of the entire attack movement
    [SerializeField] private float easeOutDurationFraction = 0.6f; // Fraction of the duration for easing out
    [SerializeField] private float easeInDurationFraction = 0.4f; // Fraction of the duration for easing in
    [SerializeField] private float impactPauseDuration = 0.05f; // Pause duration at the impact to emphasize it
    
    
    public void SetupCard(BaseCardData data) {
        cardData = data;
        DisplayData(gameObject);
    }
    
    public void SetupCard(MinionCardData minionData) {
        cardData = minionData;
        minionData.currentHealth = minionData.maxHealth;
        minionData.OnHealthChanged += UpdateHealthDisplay; 
        minionData.OnAttack += AttackTarget;
        minionData.OnRequestPosition += GetCardPosition;
        DisplayData(gameObject);
    }

    Vector3 GetCardPosition()
    {
        return transform.position;
    }
    public void AttackTarget(IDamageable target) {
        StartCoroutine(MoveTowardsTarget(target.GetPosition()));

    }
    
    IEnumerator MoveTowardsTarget(Vector3 targetPosition)
    {
        //TODO animationTimeBased On Distance to target
        float elapsedTime = 0.0f;
        Vector3 startPosition = transform.position;

        float easeOutDuration = attackAnimationDuration * easeOutDurationFraction;
        float easeInDuration = attackAnimationDuration * easeInDurationFraction;

        // Ease out (move towards the target)
        while (elapsedTime < easeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / easeOutDuration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        yield return new WaitForSeconds(impactPauseDuration);

        // Ease in (return to start position)
        elapsedTime = 0.0f; 
        while (elapsedTime < easeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / easeInDuration;
            transform.position = Vector3.Lerp(targetPosition, startPosition, t);
            yield return null;
        }

        transform.position = startPosition; 
    }
    
    private void Update() {
        if (cardData is MinionCardData)
        {
            MinionCardData minionCardData = (MinionCardData)cardData;
            string newHealthText = minionCardData.currentHealth.ToString();

            if (_healthText != newHealthText)
            {
                _healthText = newHealthText;
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
