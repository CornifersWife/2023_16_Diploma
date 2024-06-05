using Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Cards {
    public class CardDisplay : MonoBehaviour {
        [Header("Cards Elements")]
        [SerializeField] private GameObject imageObject;

        [SerializeField] public GameObject cardNameObject;
        [SerializeField] public GameObject descriptionObject;
        [SerializeField] public GameObject attackObject;
        [SerializeField] public GameObject healthObject;

        [SerializeField] private GameObject minionOnlyElements;
        
        [Space(10)]
        [SerializeField] public CanvasGroup frontOfCard;
        [SerializeField] public Image backOfCard;
        public bool frontVisible = false;
        
        [Header("Cards Set")]
        [SerializeField] public GameObject cardSetSymbolObject;

        [Header("Type")]
        [SerializeField] public GameObject cardTypeObject;

        
        private Image image;
        private TextMeshProUGUI cardName;
        private TextMeshProUGUI description;
        private TextMeshProUGUI attack;
        private TextMeshProUGUI health;
        private Image cardSetSymbol;
        private Image cardType;

        void Awake() {
            image = imageObject.GetComponent<Image>();
            cardName = cardNameObject.GetComponent<TextMeshProUGUI>();
            description = descriptionObject.GetComponent<TextMeshProUGUI>();
            attack = attackObject.GetComponent<TextMeshProUGUI>();
            health = healthObject.GetComponent<TextMeshProUGUI>();
            cardSetSymbol = cardSetSymbolObject.GetComponent<Image>();
            cardType = cardTypeObject.GetComponent<Image>();
            
            frontOfCard.enabled = !frontVisible;
            backOfCard.enabled = !frontVisible;
        }

        public void SetCardData(MinionData minionData) {
            image.sprite = minionData.Sprite;
            cardName.text = minionData.name;
            description.text = minionData.description;
            attack.text = minionData.Attack.ToString();
            health.text = minionData.MaxHealth.ToString();
            // TODO IMPLEMENT cardSetSymbol.sprite and colors
        }
        public void SetCardData(SpellData spellData) {
            image.sprite = spellData.Sprite;
            cardName.text = spellData.name;
            description.text = spellData.description;
            minionOnlyElements.GetComponent<CanvasGroup>().alpha = 0;
            // TODO IMPLEMENT cardSetSymbol.sprite and colors
        }

        public void ChangeCardVisible(bool visible) {
            frontOfCard.enabled = !visible;
            backOfCard.enabled = !visible;
            frontVisible = visible;
        }
        
        public void ChangeCardVisible() {
            frontOfCard.enabled = !frontVisible;
            backOfCard.enabled = !frontVisible;
            frontVisible = !frontVisible;
        }
    }
}
