using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour {
    public BaseCardData cardData;
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI cardHealthText;
    [SerializeField] private TextMeshProUGUI cardAttackText;
    private String healthText;

    public delegate void OnDestroyedDelegate();

    public event OnDestroyedDelegate OnDestroyed;

    private bool inAnimation;

    [Header("animations")] [SerializeField]
    private float distanceImportance = 1f;

    [SerializeField] private float attackAnimationDuration = 0.2f; // Duration of the entire attack movement
    [SerializeField] private float easeOutDurationFraction = 0.6f; // Fraction of the duration for easing out
    [SerializeField] private float easeInDurationFraction = 0.4f; // Fraction of the duration for easing in
    [SerializeField] private float impactPauseDuration = 0.05f; // Pause duration at the impact to emphasize it
    [Header("death animations")] 
    [SerializeField] private float deathAnimationRotationDuration = 0.2f;
    [SerializeField] private float deathAnimationStopDuration = 0.6f;

    public void SetupCard(BaseCardData data) {
        if (data is MinionCardData minionCardData) {
            SetupCard(minionCardData);
            return;
        }

        cardData = data;
        DisplayData(gameObject);
    }

    public void SetupCard(MinionCardData minionData) {
        cardData = minionData;
        minionData.currentHealth = minionData.maxHealth;
        minionData.OnHealthChanged += UpdateHealthDisplay;
        minionData.OnAttack += AttackTarget;
        minionData.OnRequestPosition += GetCardPosition;
        minionData.OnDeath += Destroy;
        DisplayData(gameObject);
    }

    Vector3 GetCardPosition() {
        return transform.position;
    }


    private void AttackTarget(IDamageable target) {
        StartCoroutine(MoveTowardsTarget(target.GetPosition()));
    }

    //TODO Change how it works the code is awful
    //TODO move to a separate component
    IEnumerator MoveTowardsTarget(Vector3 targetPosition) {
        inAnimation = true;
        targetPosition.y += 0.1f; //avoids mesh fighting
        float elapsedTime = 0.0f;
        Vector3 startPosition = transform.position;
        float distanceToTarget = Vector3.Distance(startPosition, targetPosition);
        float distanceFactor = distanceImportance * (float)Math.Sqrt(distanceToTarget);
        attackAnimationDuration *= distanceFactor;
        float easeOutDuration = easeOutDurationFraction * attackAnimationDuration;
        float easeInDuration = easeInDurationFraction * attackAnimationDuration;

        // Ease out 
        while (elapsedTime < easeOutDuration) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / easeOutDuration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        yield return new WaitForSeconds(impactPauseDuration);

        // Ease in 
        elapsedTime = 0.0f;
        while (elapsedTime < easeInDuration) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / easeInDuration;
            transform.position = Vector3.Lerp(targetPosition, startPosition, t);
            yield return null;
        }

        transform.position = startPosition;
        inAnimation = false;
    }

    private void OnDestroy() {
        OnDestroyed?.Invoke();
    }

    private void Destroy(MinionCardData minionData) {
        StartCoroutine(DeathSequence());
        minionData.OnHealthChanged -= UpdateHealthDisplay;
        minionData.OnAttack -= AttackTarget;
        minionData.OnRequestPosition -= GetCardPosition;
        minionData.OnDeath -= Destroy;
    }

    private IEnumerator DeathSequence() {
        yield return new WaitUntil(() => !inAnimation);
        yield return StartCoroutine(DeathAnimation());

        Destroy(gameObject);
    }

    private IEnumerator DeathAnimation() {
        float time = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 30, 0);

        while (time < deathAnimationRotationDuration) {
            time += Time.deltaTime;
            float t = time / deathAnimationRotationDuration;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        transform.rotation = endRotation;
        yield return new WaitForSeconds(deathAnimationStopDuration);
    }

    private void Update() {
        if (cardData is MinionCardData minionCardData) {
            string newHealthText = minionCardData.currentHealth.ToString();

            if (healthText != newHealthText) {
                healthText = newHealthText;
                UpdateHealthDisplay(minionCardData.currentHealth);
            }
        }
    }
    
    private void UpdateHealthDisplay(int newHealth) {
        healthText = newHealth.ToString();
        cardHealthText.text = healthText;
    }
    
    private void DisplayData(GameObject card) {
        cardNameText.text = cardData.cardName;

        if (cardData is MinionCardData minionCardData) {
            cardAttackText.text = minionCardData.power.ToString();
            cardHealthText.text = minionCardData.currentHealth.ToString();
            healthText = cardHealthText.text;
        }

        //If card is not Minion:
        else {
            cardAttackText.text = "";
            cardHealthText.text = "";
        }
    }
}