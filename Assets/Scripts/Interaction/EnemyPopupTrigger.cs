using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPopupTrigger : MonoBehaviour, IPointerClickHandler {
    [SerializeField] private GameObject enemyPanel;
    public void OnPointerClick(PointerEventData eventData) {
        if (PauseManager.Instance.IsOpen)
            return;
        Enemy enemy = gameObject.GetComponent<EnemySM>().GetEnemy();
        if (enemy.GetState() == EnemyState.Undefeated) {
            enemyPanel.SetActive(true);
            enemyPanel.transform.GetChild(0).gameObject.SetActive(true);
            EnemyPopup.Instance.Enemy = enemy;
            InputManager.Instance.DisableAllInput();
        }
    }
}
