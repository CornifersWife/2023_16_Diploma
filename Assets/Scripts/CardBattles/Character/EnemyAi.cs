using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CardBattles.CardScripts;
using CardBattles.Managers;
using NaughtyAttributes;
using UnityEngine;
using Random = System.Random;

namespace CardBattles.Character {
    public class EnemyAi : MonoBehaviour {
        private CharacterManager character;
        private Random random;

        [SerializeField] private float waitBetweenPlayingCards;
        [SerializeField, Range(0f, 1f)] private float chanceToEndAfterEachAction;

        [SerializeField] private StringFloatDictionary baseWeights;

        private List<Card> CardsInHand => character.hand.Cards.Where(e => e is Minion).ToList();
        
            
        [BoxGroup("Debug"),SerializeField] private bool showInnerDialogue;//debug
        
        private StringFloatDictionary weights;
        
        private void Awake() {
            random = new Random();
            character = GetComponent<CharacterManager>();
            CopyWeightValues();
        }

        private void CopyWeightValues() {
            weights = new StringFloatDictionary();
            foreach (var key in baseWeights.Keys) {
                weights.Add(key,baseWeights[key]);
            }
        }
        
        public IEnumerator PlayTurn() {
            CopyWeightValues();

            /*if (showInnerDialogue) 
                Debug.Log($"Can do next move {!CantDoNextAction()}");
                */
            
            if (CantDoNextAction()) {
                yield break;
            }

            var nextAction = ChooseActionToDo();

            switch (nextAction) {
                case "Draw":
                    yield return character.Draw(1, 1);
                    break;
                case "Play":
                    yield return PlayACard();
                    break;
                default:
                    Debug.LogError("Enemy tried to do forbidden action");
                    break;
            }

            if (random.NextDouble() > chanceToEndAfterEachAction)
                yield return PlayTurn();
        }

        private bool CantDoNextAction() {
            return NoMoreActions() || TurnManager.Instance.gameHasEnded || !character.IsYourTurn;
        }

        private bool NoMoreActions() {
            bool deck = character.deck.cards.Any();
            bool hand = CardsInHand.Any();
            
            //TODO account for 0-mana cost cards
            bool mana = character.manaManager.CurrentMana > 0;

            //TODO change this when you can do stuff without mana
            return (!deck && !hand) || !mana;
        }

        private string ChooseActionToDo() {
            ModifyProbabilities();
            float totalWeight = 0;
            foreach (var item in weights) {
                totalWeight += item.Value;
            }

            double randomValue = random.NextDouble() * totalWeight;


            foreach (var item in weights.Keys) {
                randomValue -= weights[item];
                if (randomValue <= 0) {
                    return item;
                }
            }

            //the default
            return "Draw";
        }

        private IEnumerator PlayACard() {
            var hand = CardsInHand;
            var cardSpots = character.boardSide.GetEmptyCardSpots();


            if (!hand.Any() || !cardSpots.Any()) {
                Debug.LogError("this shouldn't happen");
                yield break;
            }

            var card = hand[random.Next(hand.Count)];
            while (card is not Minion) {
                card = hand[random.Next(hand.Count)];
            }
                
            var cardSpot = cardSpots[random.Next(cardSpots.Count)];

            yield return character.PlayCardCoroutine(card, cardSpot, waitBetweenPlayingCards);
        }


        //TODO MAGIC NUMBERS
        private void ModifyProbabilities() {
            if (CardsInHand.Count == 0) {
                weights = new StringFloatDictionary { { "Draw", 1f } };
                return;
            }

            if (CardsInHand.Count <= 1)
                weights["Draw"] *= 2;

            if (!character.boardSide.GetEmptyCardSpots().Any()) {
                weights["Play"] = 0;
            }
        }
    }
}