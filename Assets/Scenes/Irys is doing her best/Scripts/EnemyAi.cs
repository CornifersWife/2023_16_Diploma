using System.Collections;
using System.Linq;
using NaughtyAttributes;
using Scenes.Irys_is_doing_her_best.Scripts.My.Board;
using UnityEngine;
using Random = System.Random;

namespace Scenes.Irys_is_doing_her_best.Scripts {
    public class EnemyAi : MonoBehaviour {
        private CharacterManager character;
        private Random random;

        [SerializeField] private float waitBetweenPlayingCards;
        [SerializeField, Range(0f, 1f)] private float chanceToEndAfterEachAction;

        [SerializeField] private StringFloatDictionary baseWeights;

        private StringFloatDictionary weights;

        private StringFloatDictionary percentageWeights;

        [ShowNativeProperty] private StringFloatDictionary PercentageWeights => percentageWeights;


        private void Awake() {
            random = new Random();
            character = GetComponent<CharacterManager>();
            weights.CopyFrom(baseWeights);

            CalculatePercentageWeights();
        }

        private void CalculatePercentageWeights() {
            percentageWeights.CopyFrom(weights);

            var sum = weights.Sum(e => e.Value);

            foreach (var entry in percentageWeights.Keys) {
                percentageWeights[entry] /= sum;
            }
        }


        public IEnumerator PlayTurn() {
            weights.CopyFrom(baseWeights);

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
            bool deck = character.deck.Cards.Any();
            bool hand = character.hand.Cards.Any();
            bool mana = character.manaManager.CurrentMana <= 0;

            return (!deck && !hand && !mana);
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
            var hand = character.hand.Cards;
            var cardSpots = character.boardSide.GetEmptyCardSpots();


            if (!hand.Any() || !cardSpots.Any()) {
                Debug.LogError("this shouldn't happen");
                yield break;
            }

            var card = hand[random.Next(hand.Count)];
            var cardSpot = cardSpots[random.Next(cardSpots.Count)];

            yield return character.PlayCardCoroutine(card, cardSpot, waitBetweenPlayingCards);
        }


        //TODO MAGIC NUMBERS
        private void ModifyProbabilities() {
            if (character.hand.Cards.Count == 0) {
                weights = new StringFloatDictionary { { "Draw", 1f } };
                return;
            }

            if (character.hand.Cards.Count <= 1)
                weights["Draw"] *= 2;

            if (!character.boardSide.GetEmptyCardSpots().Any()) {
                weights["Play"] = 0;
            }

            CalculatePercentageWeights();
        }
    }
}