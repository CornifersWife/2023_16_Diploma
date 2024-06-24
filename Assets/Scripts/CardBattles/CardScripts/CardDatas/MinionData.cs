using NaughtyAttributes;
using UnityEngine;

namespace CardBattles.CardScripts.CardDatas {
    public class MinionData : CardData {
        [Space(30), Header("Minion")] 
        [BoxGroup("Data")][Min(0)] [SerializeField]
        public int Attack;

        [BoxGroup("Data")] [SerializeField] [Min(1)]
        public int MaxHealth = 1;
    }
}