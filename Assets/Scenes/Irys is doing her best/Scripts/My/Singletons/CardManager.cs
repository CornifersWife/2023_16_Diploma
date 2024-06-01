using Scenes.Irys_is_doing_her_best.Scripts.My;
using Scenes.Irys_is_doing_her_best.Scripts.My.Card;
using UnityEngine;

public class CardManager : MonoBehaviour {
    public static CardManager Instance { get; private set; }

    public GameObject minionPrefab;
    public GameObject spellPrefab;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: makes the manager persist across scenes
        } else {
            Destroy(gameObject);
        }
    }

    public Card CreateCard(CardData cardData) {
        GameObject cardObject = null;
        Card cardComponent = null;

        if (cardData is MinionData) {
            cardObject = Instantiate(minionPrefab);
            cardComponent = cardObject.GetComponent<Minion>();
        } else if (cardData is SpellData) {
            cardObject = Instantiate(spellPrefab);
            cardComponent = cardObject.GetComponent<Spell>();
        }

        if (cardComponent != null) {
            cardComponent.Initialize(cardData);
        } else {
            Debug.LogError("Failed to create card.");
        }

        return cardComponent;
    }
}