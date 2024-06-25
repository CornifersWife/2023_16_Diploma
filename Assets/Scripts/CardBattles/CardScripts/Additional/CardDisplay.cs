using CardBattles.CardScripts.CardDatas;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CardBattles.CardScripts.Additional {
    public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [Foldout("Card scale"), SerializeField]
        public float scaleInHand = 0.9f;

        [Foldout("Card scale"), SerializeField]
        private float scaleOnBoard = 1f;

        [Foldout("Card scale"), SerializeField]
        private float scaleOnHover = 1.1f;

        private float currentScale = 1f;

        [Header("Front/Back")] [Foldout("Objects")] [SerializeField]
        private CanvasGroup frontOfCard;

        [Foldout("Objects")] [SerializeField] private Image backOfCard;

        [Space, Header("Elements")] [Foldout("Objects")] [SerializeField]
        private TextMeshProUGUI cardName;

        [Foldout("Objects")] [SerializeField] private TextMeshProUGUI description;
        [Foldout("Objects")] [SerializeField] private TextMeshProUGUI attack;
        [Foldout("Objects")] [SerializeField] private TextMeshProUGUI health;
        [Foldout("Objects")] [SerializeField] private Image cardImage;
        [Foldout("Objects")] [SerializeField] private Image cardSetSymbol;
        [Foldout("Objects")] [SerializeField] private Image cardType;

        [Foldout("Objects")] [SerializeField] private CanvasGroup minionOnlyElements;

        public bool frontVisible;

        void Awake() {
            frontOfCard.enabled = !frontVisible;
            backOfCard.enabled = !frontVisible;
        }

        public void SetCardData(MinionData minionData) {
            cardImage.sprite = minionData.sprite;
            cardName.text = minionData.name;
            description.text = minionData.description;
            attack.text = minionData.attack.ToString();
            health.text = minionData.maxHealth.ToString();
            // TODO IMPLEMENT cardSetSymbol.sprite and colors
        }

        public void SetCardData(SpellData spellData) {
            cardImage.sprite = spellData.sprite;
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

        public void IsPlacedOnBoard(bool isNotNull) {
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