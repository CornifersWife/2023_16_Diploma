using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas;
using Scenes.Irys_is_doing_her_best.Scripts.My.Singletons;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class DeckManager : MonoBehaviour {
        [SerializeField] private List<CardSetData> cardSetDatas = new List<CardSetData>();
        private SerializableDictionary<string, List<Cards.Card>> cardSets = new SerializableDictionary<string, List<Cards.Card>>();
        public List<Cards.Card> Cards = new List<Cards.Card>();
        public bool isPlayer=false;

        private void Start() {
            try {
                isPlayer = GetComponentInParent<CharacterManager>().isPlayer;
            }
            catch (Exception e) {
                Debug.LogException(e);
            }
            if (!Application.isEditor)
                cardSetDatas = LoadCardSetData();

            // Debug logging to check the state of cardSetDatas
            if (cardSetDatas == null) {
                Debug.LogError("cardSetDatas is null");
            } else {
                Debug.Log("cardSetDatas is not null and contains " + cardSetDatas.Count + " elements");
            }

            CreateCardSetsFromData(); //line 24
            CreateCardFromDeck();

        }

        private IEnumerator Sleep1() {
            yield return new WaitForEndOfFrame();
        }

        /*
        private void Start() {
        }*/

        private List<CardSetData> LoadCardSetData() {
            if (isPlayer)
                return InventoryController.Instance.GetCardSets();
            return EnemyStateManager.Instance.GetCurrentEnemy().GetDeck();
        }

        private void CreateCardSetsFromData(int t) {
            int i = 0;
            foreach (var cardSetData in cardSetDatas) {
                i++;
                
                // Debug logging to check the state of each cardSetData
                if (cardSetData == null) {
                    Debug.LogError("cardSetData is null at index " + i);
                    continue;
                }

                if (cardSetData.cards == null) {
                    Debug.LogError("cardSetData.cards is null for cardSetData: " + cardSetData.displayName);
                    continue;
                }

                foreach (var cardData in cardSetData.cards) {
                    var card = CardManager.Instance.CreateCard(cardData, gameObject.transform,isPlayer); 
                    //TODO not sure if needed
                    //card.GetComponent<CardDisplay>().enabled = false;
                    
                    cardSets.Add(i + ". " + cardSetData.name, card);
                }
            }
        }
        private void CreateCardSetsFromData() {
            int i = 0;
            foreach (var cardSetData in cardSetDatas) {
                i++;
                // Debug logging to check the state of each cardSetData
                if (cardSetData == null) {
                    Debug.LogError("cardSetData is null at index ");
                    continue;
                }

                if (cardSetData.cards == null) {
                    Debug.LogError("cardSetData.cards is null for cardSetData: " + cardSetData.displayName);
                    continue;
                }

                foreach (var cardData in cardSetData.cards) {
                    if (cardData == null) {
                        Debug.LogError("cardData is null in cardSetData: " + cardSetData.displayName);
                        continue;
                    }
                   
                    var card = CardManager.Instance.CreateCard(cardData,gameObject.transform,isPlayer);//CreateCard(cardData,gameObject.transform);
                   

                    if (card == null) {
                        Debug.LogError("Card creation failed for cardData in cardSetData: " + cardSetData.displayName);
                        continue;
                    }

                    cardSets.TryAdd(cardSetData.displayName + i,new List<Cards.Card>());
                    cardSets[cardSetData.displayName+i].Add(card);
                }
            }
        }


        private void CreateCardFromDeck() {
            var cardLists = cardSets.Values.ToList();
            var allCards = new List<Cards.Card>();
            foreach (var _ in cardLists) {
                allCards.AddRange(_);
            }
            var shuffledList = allCards.OrderBy(_ => Guid.NewGuid()).ToList(); //randomly shuffles

            Cards.AddRange(shuffledList);
        }
        
    }
}
