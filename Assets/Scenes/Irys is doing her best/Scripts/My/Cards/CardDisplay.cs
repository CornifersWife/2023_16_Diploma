using System;
using DG.Tweening;
using NaughtyAttributes;
using Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Cards {
    public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [Header("Cards Elements")] [SerializeField]
        private GameObject imageObject;

        [SerializeField] private GameObject cardNameObject;
        [SerializeField] private GameObject descriptionObject;
        [SerializeField] private GameObject attackObject;
        [SerializeField] private GameObject healthObject;
        [SerializeField] private GameObject minionOnlyElements;

        [Space(10)] [SerializeField] private CanvasGroup frontOfCard;
        [SerializeField] private Image backOfCard;
        public bool frontVisible = false;

        [Header("Cards Set")] [SerializeField] public GameObject cardSetSymbolObject;

        [Header("Type")] [SerializeField] public GameObject cardTypeObject;

        [BoxGroup("Card scale"), SerializeField]
        public float scaleInHand = 0.9f;

        [BoxGroup("Card scale"), SerializeField]
        private float scaleOnBoard = 1f;

        [BoxGroup("Card scale"), SerializeField]
        private float scaleOnHover = 1.1f;

        private float currentScale = 1f;


        private Image image;
        private TextMeshProUGUI cardName;
        private TextMeshProUGUI description;
        private TextMeshProUGUI attack;
        private TextMeshProUGUI health;
        private Image cardSetSymbol;
        private Image cardType;

        void Awake() {
            //TODO idk why it forced me to do it this way, i could not assign it in inspector
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
            transform.DORotate(new Vector3(0, 0, 0), 0.1f);
        }

        public void ChangeCardVisible() {
            frontOfCard.enabled = !frontVisible;
            backOfCard.enabled = !frontVisible;
            frontVisible = !frontVisible;
        }


        [SerializeField] private float scaleOnHoverTime = 0.1f;

        public void OnPointerEnter(PointerEventData eventData) {
            //TODO add ability to drop some cards on top of other, like a buff
            if (!frontVisible || eventData.pointerDrag is not null) {
                return;
            }
            transform.DOScale(scaleOnHover, 
                scaleOnHoverTime);
        }

        public void OnPointerExit(PointerEventData eventData) {
            if (!frontVisible || eventData.pointerDrag is not null) {
                return;
            }

            transform.DOScale(Vector3.one * currentScale, scaleOnHoverTime);
        }

        public void IsInPlay(bool isNotNull) {
            if (!isNotNull) {
                ChangeCurrentScale(1f);
                return;
            }

            ChangeCurrentScale(scaleOnBoard);
        }

        public void IsDrawn() {
            ChangeCurrentScale(scaleInHand);
        }

        private void ChangeCurrentScale(float scale) {
            currentScale = scale;
            transform.DOScale(currentScale, 0.3f);
        }
    }
}