using System.Linq;
using Scenes.Irys_is_doing_her_best.Scripts.My.Cards;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class CharacterManager : MonoBehaviour {
        [Header("Data")]
        public HandManager hand;
        public DeckManager deck;
        public Hero hero;
        public BoardSide boardSide;
        public bool isPlayer;

        public UnityEvent<CardSpot> getCardSpots;

        
        public void Draw() {
            if (!deck.Cards.Any()) {
                //TODO implement feedback
                Debug.LogError("No more cards honey");
                return;
            }

            var card = deck.Cards[0];
            card.GetComponent<CardDisplay>().ChangeCardVisible(isPlayer);
            hand.Cards.Add(card);
            card.transform.SetParent(hand.transform);
            deck.Cards.RemoveAt(0);
        }
    }
}
