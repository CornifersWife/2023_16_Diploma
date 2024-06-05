using System.Linq;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class CharacterManager : MonoBehaviour {
        [Header("Data")]
        public HandManager hand;
        public DeckManager deck;
        public Hero hero;
        public BoardSide boardSide;
        public bool isPlayer;

    
        public void Draw() {
            if (!deck.Cards.Any()) {
                //TODO implement something
                Debug.LogError("No more cards honey");
                return;
            }
            hand.Cards.Add(deck.Cards[0]);
            deck.Cards.RemoveAt(0);
            
        }
    }
}
