using TMPro;
using UnityEngine;

public class HeroHealth : MonoBehaviour {
        private GameObject hero;
        private int health;
        public TextMeshPro showHealth;

        //Use at the beginning of the game
        public void gameStart() {
                hero = GetComponent<GameObject>();
                showHealth = GetComponent<TextMeshPro>();
                health = 20;
                showHealth.text = "HP: " + health;
        }

        //Used when hero is attack
        public void attaked(MinionCardData card) {
                health -= card.power;
                if (health <= 0) {
                        Destroy(hero);
                }
                showHealth.text = "HP: " + health;
        }
}