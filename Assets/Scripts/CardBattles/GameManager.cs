using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scenes.Irys_is_doing_her_best.Scripts;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    private bool gameOver = false;
    [FormerlySerializedAs("playerHero")] public HeroOld playerHeroOld;
    public DeckManagerOld playerDeck;
    public HandManager playerHand;
    public ActionPointManager playerActionPoint;

    [FormerlySerializedAs("enemyHero")] [Space] public HeroOld enemyHeroOld;
    public DeckManagerOld enemyDeck;
    public HandManager enemyHand;
    public ActionPointManager enemyActionPoint;
    [FormerlySerializedAs("board")] [Space] public BoardOld boardOld;
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
        foreach (var cardSpot in FindObjectsOfType<CardSpotOld>()) {
            SubscribeToCardSpot(cardSpot);
        }
    }

    public CardOld CreateCardInstance(BaseCardData cardData) {
        GameObject cardObj = Instantiate(cardPrefab, transform);
        CardOld newCardOld = cardObj.GetComponent<CardOld>();
        newCardOld.SetupCard(Instantiate(cardData));
        return newCardOld;
    }

    public CardOld CreateCardInstance(BaseCardData cardData, Transform newTransform) {
        GameObject cardObj = Instantiate(cardPrefab, transform);
        cardObj.transform.position = newTransform.position;
        CardOld newCardOld = cardObj.GetComponent<CardOld>();
        newCardOld.SetupCard(Instantiate(cardData));
        return newCardOld;
    }

    private void OnCardPlayed(CardSpotOld cardSpotOld, CardOld cardOld) {
        var actionPoint = cardSpotOld.isPlayers ? playerActionPoint : enemyActionPoint;
        if (!actionPoint.CanUseAP()) {
            cardOld.GetComponent<DragAndDrop>().SnapBack();
            return;
        }

        actionPoint.UseActionPoint();
        var hand = cardSpotOld.isPlayers ? playerHand : enemyHand;
        cardSpotOld.SetCardDisplay(cardOld);
        hand.RemoveCardFromHand(cardOld);
        Transform cardTransform;
        (cardTransform = cardOld.transform).SetParent(cardSpotOld.transform);
        cardTransform.localPosition = Vector3.zero;
        cardTransform.position = cardSpotOld.transform.position;
        cardOld.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void SubscribeToCardSpot(CardSpotOld cardSpotOld) {
        cardSpotOld.Play += OnCardPlayed;
    }

    public void PlayerDrawCard() {
        BaseCardData drawnCard = playerDeck.DrawCard();
        if (drawnCard != null) {
            playerHand.AddCardToHand(drawnCard);
        }
    }

    public bool EnemyPlayMinion() {
        var availableCards = enemyHand.hand
            .Where(card =>/* card.cardData is MinionCardData && */enemyActionPoint.CanUseAP()).ToArray();
        var availableBoardSpaces = boardOld.enemyMinions
            .Where(space => space.IsEmpty()).ToArray();
        if (availableCards.Length <= 0 || availableBoardSpaces.Length <= 0) {
            return false;
        }

        var card = availableCards[Random.Range(0, availableCards.Count())];
        var boardSpot = availableBoardSpaces[Random.Range(0, availableBoardSpaces.Count())];
        boardSpot.CardOld = card;
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
        boardOld.MinionsAttack(true);
    }

    public void EnemyAttack() {
        boardOld.MinionsAttack(false);
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
        SceneSwitcher.Instance.LoadScene("beta-release-2");
    }

    private IEnumerator WinnerImageAndLoadScene() {
        gameOver = true;
        winImage.enabled = true;
        yield return new WaitForSeconds(3);
        winImage.enabled = false;

        EnemyStateManager.Instance.ChangeEnemyState(EnemyState.Defeated);
        SceneSwitcher.Instance.LoadScene("beta-release-2");
    }
    
    
    
    public List<IDamageable> GetYourMinions(bool belongsToPlayer) { //untested
        return boardOld.GetMinons(belongsToPlayer);
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
        return belongsToPlayer ? playerHeroOld : enemyHeroOld;
    }

    public IDamageable GetEnemyHero(bool belongsToPlayer) { //untested
        return GetYourHero(!belongsToPlayer);
    }

    public List<IDamageable> GetAllHeroes() { //untested
        return new List<IDamageable> { playerHeroOld, enemyHeroOld };
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