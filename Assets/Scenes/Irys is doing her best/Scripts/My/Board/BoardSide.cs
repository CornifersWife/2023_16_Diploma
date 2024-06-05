using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class BoardSide : MonoBehaviour {
        [SerializeField] public CardSpot[] cardSpots = new CardSpot[4];
        [SerializeField] public bool isPlayers;
        void Start()
        {
        
        }
    }
}
