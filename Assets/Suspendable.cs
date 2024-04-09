
using UnityEngine;
using UnityEngine.SceneManagement;

public class Suspendable : MonoBehaviour {
    private static Suspendable instance = null;

    public GameObject cameraController;
    public GameObject inventoryController;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (SceneManager.GetActiveScene().name == "TEST1 Overworld") {
            cameraController.SetActive(true);
            inventoryController.SetActive(true);

            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else {
            cameraController.SetActive(false);
            inventoryController.SetActive(false);
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
