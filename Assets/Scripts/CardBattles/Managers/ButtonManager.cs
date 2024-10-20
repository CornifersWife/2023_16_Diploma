using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CardBattles.Managers {
    public class ButtonManager : MonoBehaviour {
        public static ButtonManager Instance { get; private set; }
        private bool buttonsEnabled;
        private bool drawButtonCooldown;

        private void Awake() {
            if (Instance is null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        private void Start() {
            var childrenButtons = GetComponentsInChildren<Button>();
            foreach (var button in childrenButtons) {
                if (!buttons.Contains(button))
                    buttons.Add(button);
            }

            drawButton.onClick.AddListener(OnDrawButtonClick);
            StartCoroutine(CheckForButtonEnabled());
        }

        [SerializeField] private List<Button> buttons;


        [BoxGroup("Draw"), SerializeField] private Button drawButton;
        [BoxGroup("Draw"), SerializeField] private float drawButtonCooldownTime = 0.5f;

        [InfoBox("Make sure to add onclick events here, not in the button")] [BoxGroup("Draw"), SerializeField]
        private UnityEvent drawButtonEvent;

        private IEnumerator CheckForButtonEnabled() {
            do {
                if (buttonsEnabled != TurnManager.Instance.isPlayersTurn)
                    ButtonsEnabled(TurnManager.Instance.isPlayersTurn);
                yield return new WaitForSeconds(0.1f);
            } while (!TurnManager.Instance.gameHasEnded);
        }

        private void ButtonsEnabled(bool value) {
            buttonsEnabled = value;
            foreach (var button in buttons) {
                button.enabled = value;
            }
        }

        private void OnDrawButtonClick() {
            if (!drawButtonCooldown) {
                drawButtonEvent.Invoke();
                StartCoroutine(DrawButtonCooldownRoutine());
            }
        }


        private IEnumerator DrawButtonCooldownRoutine() {
            drawButtonCooldown = true;
            yield return new WaitForSeconds(drawButtonCooldownTime);
            drawButtonCooldown = false;
        }

        //TODO add some fancy shmansy OnHover, OnHoverExit Actions that allow mana to highlight
    }
}