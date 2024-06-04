using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scenes.Irys_is_doing_her_best.Scripts.My.Card;
using Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas;
using Scenes.Irys_is_doing_her_best.Scripts.My.Singletons;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My {
    public class DeckManager : MonoBehaviour {
        [SerializeField] private List<CardSetData> cardSetDatas;

        private Dictionary<string, Cards.Card> cardSets = new Dictionary<string, Cards.Card>();

        public List<Cards.Card> cards;
        public bool isPlayer;

        private void Awake() {
            if (!Application.isEditor)
                cardSetDatas = LoadCardSetData();

            StartCoroutine(Sleep1());

            CreateCardSetsFromData();
        }

        private IEnumerator Sleep1() {
            yield return new WaitForEndOfFrame();
        }

        private void Start() {
            CreateCardFromDeck();
        }

        private List<CardSetData> LoadCardSetData() {
            if (isPlayer)
                return InventoryController.Instance.GetCardSets();
            return EnemyStateManager.Instance.GetCurrentEnemy().GetDeck();
        }

        private void CreateCardSetsFromData() {
            int i = 0;
            foreach (var cardSetData in cardSetDatas) {
                i++;
                foreach (var cardData in cardSetData.cards) {
                    var card = CardManager.Instance.CreateCard(cardData, gameObject);
                    //TODO not sure if needed
                    card.GetComponent<CardDisplay>().enabled = false;
                    //
                    cardSets.Add(i + ". " + cardSetData.name, card);
                }
            }
        }

        private void CreateCardFromDeck() {
            var tmp = cardSets.Values.ToList();
            var shuffledList = tmp.OrderBy(_ => Guid.NewGuid()).ToList(); //randomly shuffles

            cards.AddRange(shuffledList);
        }
    }
}