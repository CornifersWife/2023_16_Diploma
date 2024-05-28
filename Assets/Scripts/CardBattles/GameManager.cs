using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    private bool gameOver = false;
    public Hero playerHero;
    public DeckManager playerDeck;
    public HandManager playerHand;
    public ActionPointManager playerActionPoint;

    [Space] public Hero enemyHero;
    public DeckManager enemyDeck;
    public HandManager enemyHand;
    public ActionPointManager enemyActionPoint;
    [Space] public Board board;
    public GameObject cardPrefab;

    public static GameManager Instance { get; private set; }

    [Header("Win/Lose Images")] [SerializeField]
    private RawImage loseImage;

    [SerializeField] private RawImage winImage;


    private void Awake() {
        if (Instance is not null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
    }

    void Start() {
        foreach (var cardSpot in FindObjectsOfType<CardSpot>()) {
            SubscribeToCardSpot(cardSpot);
        }
    }

    public CardDisplay CreateCardInstance(BaseCardData cardData) {
        GameObject cardObj = Instantiate(cardPrefab, transform);
        CardDisplay newCardDisplay = cardObj.GetComponent<CardDisplay>();
        newCardDisplay.SetupCard(Instantiate(cardData));
        return newCardDisplay;
    }

    public CardDisplay CreateCardInstance(BaseCardData cardData, Transform newTransform) {
        GameObject cardObj = Instantiate(cardPrefab, transform);
        cardObj.transform.position = newTransform.position;
        CardDisplay newCardDisplay = cardObj.GetComponent<CardDisplay>();
        newCardDisplay.SetupCard(Instantiate(cardData));
        return newCardDisplay;
    }

    private void OnCardPlayed(CardSpot cardSpot, CardDisplay cardDisplay) {
        var actionPoint = cardSpot.isPlayers ? playerActionPoint : enemyActionPoint;
        if (!actionPoint.CanUseAP()) {
            cardDisplay.GetComponent<DragAndDrop>().SnapBack();
            return;
        }

        actionPoint.UseActionPoint();
        var hand = cardSpot.isPlayers ? playerHand : enemyHand;
        cardSpot.SetCardDisplay(cardDisplay);
        hand.RemoveCardFromHand(cardDisplay);
        Transform cardTransform;
        (cardTransform = cardDisplay.transform).SetParent(cardSpot.transform);
        cardTransform.localPosition = Vector3.zero;
        cardTransform.position = cardSpot.transform.position;
        cardDisplay.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void SubscribeToCardSpot(CardSpot cardSpot) {
        cardSpot.Play += OnCardPlayed;
    }

    public void PlayerDrawCard() {
        BaseCardData drawnCard = playerDeck.DrawCard();
        if (drawnCard != null) {
            playerHand.AddCardToHand(drawnCard);
        }
    }

    public bool EnemyPlayMinion() {
        var availableCards = enemyHand.hand
            .Where(card => card.cardData is MinionCardData && enemyActionPoint.CanUseAP()).ToArray();
        var availableBoardSpaces = board.enemyMinions
            .Where(space => space.IsEmpty()).ToArray();
        if (availableCards.Length <= 0 || availableBoardSpaces.Length <= 0) {
            return false;
        }

        var card = availableCards[Random.Range(0, availableCards.Count())];
        var boardSpot = availableBoardSpaces[Random.Range(0, availableBoardSpaces.Count())];
        boardSpot.CardDisplay = card;
        return true;
    }

    public void EnemyDrawCard() {
        BaseCardData drawnCard = enemyDeck.DrawCard();
        if (drawnCard != null) {
            enemyHand.AddCardToHand(drawnCard);
        }
    }

    public void PlayerDrawButton() {
        if (!playerActionPoint.CanUseAP()) {
            Debug.Log("Cant use AP");
            return;
        }

        playerActionPoint.UseActionPoint();
        playerHand.DrawACard();
    }

    public void PlayerAttack() {
        board.MinionsAttack(true);
    }

    public void EnemyAttack() {
        board.MinionsAttack(false);
    }

    public void Win() {
        if (!gameOver)
            StartCoroutine(WinnerImageAndLoadScene());
    }

    public void Lose() {
        if (!gameOver)
            StartCoroutine(LoserImageAndLoadScene());
    }

    private IEnumerator LoserImageAndLoadScene() {
        gameOver = true;
        loseImage.enabled = true;
        yield return new WaitForSeconds(3);
        loseImage.enabled = false;

        EnemyStateManager.Instance.ChangeEnemyState(EnemyState.Defeated);
        ManageGame.Instance.IsAfterFirstFight = true;
        SceneSwitcher.Instance.LoadScene("beta-release");
    }

    private IEnumerator WinnerImageAndLoadScene() {
        gameOver = true;
        winImage.enabled = true;
        yield return new WaitForSeconds(3);
        winImage.enabled = false;

        EnemyStateManager.Instance.ChangeEnemyState(EnemyState.Defeated);
        ManageGame.Instance.IsAfterFirstFight = true;
        SceneSwitcher.Instance.LoadScene("beta-release");
    }
    
    
    
    public List<IDamageable> GetYourMinions(bool belongsToPlayer) { //untested
        return board.GetMinons(belongsToPlayer);
    }
    
    public List<IDamageable> GetEnemyMinions(bool belongsToPlayer) { //untested
        return GetYourMinions(!belongsToPlayer);
    }

    public List<IDamageable> GetAllMinions() { //untested
        List<IDamageable> targets = new List<IDamageable>();
        targets.AddRange(GetYourMinions(true));
        targets.AddRange(GetYourMinions(false));
        return targets;
    }

    public IDamageable GetYourHero(bool belongsToPlayer) { //untested
        return belongsToPlayer ? playerHero : enemyHero;
    }

    public IDamageable GetEnemyHero(bool belongsToPlayer) { //untested
        return GetYourHero(!belongsToPlayer);
    }

    public List<IDamageable> GetAllHeroes() { //untested
        return new List<IDamageable> { playerHero, enemyHero };
    }

    public List<IDamageable> GetAllies(bool belongsToPlayer) { //untested
        List<IDamageable> tmp = new List<IDamageable>();
        tmp.AddRange(GetYourMinions(belongsToPlayer));
        tmp.Add(GetYourHero(belongsToPlayer));
        return tmp;
    }

    public List<IDamageable> GetEnemies(bool belongsToPlayer) { //untested
        return GetAllies(!belongsToPlayer);
    }

    public List<IDamageable> GetAllDamageable() { //untested
        List<IDamageable> targets = new List<IDamageable>();
        targets.AddRange(GetAllHeroes());
        targets.AddRange(GetAllMinions());
        return targets;
    }

    public List<IDamageable> GetMinionsInSameSet(BaseCardData sourceCard) { //untested
        throw new System.NotImplementedException();
    }
}