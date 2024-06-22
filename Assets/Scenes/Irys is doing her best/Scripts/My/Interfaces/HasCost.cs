using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces {
    public class HasCost : IHasCost {
        [Min(1)]
        private int cost;

        public HasCost(int cost) {
            this.cost = cost;
        }
        public int GetCost() {
            return cost;
        }
    }
}