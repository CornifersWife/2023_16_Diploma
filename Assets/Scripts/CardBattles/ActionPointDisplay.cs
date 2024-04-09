using System;
using UnityEngine;

public class ActionPointDisplay : MonoBehaviour {
    [SerializeField] private GameObject orbPrefab; 
    private ActionPointManager actionPointManager;
    private GameObject[] orbs;

    private void Awake() {
        actionPointManager = GetComponent<ActionPointManager>();
    }

    private void Start() {
        orbs = new GameObject[actionPointManager.maxAP];
        for (int i = 0; i < actionPointManager.maxAP; i++) {
            orbs[i] = Instantiate(orbPrefab, GetPositionForOrb(i), Quaternion.identity, transform);
        }

        UpdateOrbs();
    }

    private void Update() {
        UpdateOrbs();
    }

    void UpdateOrbs() {
        for (int i = 0; i < orbs.Length; i++) {
            if (i < actionPointManager.currentAP) {
                orbs[i].SetActive(true);
            }
            else {
                orbs[i].SetActive(false);
            }
        }
    }

    Vector3 GetPositionForOrb(int index) {
        float offset = 1.0f;
        return new Vector3(transform.position.x + index * offset, transform.position.y, transform.position.z);
    }
}