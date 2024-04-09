using System;
using UnityEngine;

public class ActionPointDisplay : MonoBehaviour {
    [SerializeField] private GameObject orbPrefab;
    [SerializeField] private Transform apField;

    [SerializeField] private float distanceBetweenOrbs = 0.5f;
    
    private ActionPointManager actionPointManager;
    private GameObject[] orbs; //TODO if we want to change max mana we need to change it to list

    private void Awake() {
        actionPointManager = GetComponent<ActionPointManager>();
        actionPointManager.OnAPChanged += UpdateOrbs;
        orbs = new GameObject[actionPointManager.maxAP];
        InitializeOrbs();
    }

    private void Start() {
        UpdateOrbs();

    }

    /*
    private void OnValidate() {
        if (orbs != null && Application.isEditor && !Application.isPlaying) {
            InitializeOrbs(); // Re-initialize to adjust for the new distance
        }
    }
    */

    private void OnDestroy() {
            actionPointManager.OnAPChanged -= UpdateOrbs;
        }

    private void InitializeOrbs() {
        if (orbs != null) {
            foreach (var orb in orbs) {
                if (orb != null) DestroyImmediate(orb);
            }
        }

        for (int i = 0; i < actionPointManager.maxAP; i++) {
            orbs![i] = Instantiate(orbPrefab, GetPositionForOrb(i), Quaternion.identity, transform);
            orbs![i].transform.SetParent(apField,false);
        }
    }
    void UpdateOrbs() {
        for (int i = 0; i < orbs.Length; i++) {
            orbs![i].SetActive(i < actionPointManager.CurrentAP);
        }
    }

    Vector3 GetPositionForOrb(int index) {
        var position = transform.position;
        return new Vector3(position.x + index * distanceBetweenOrbs, position.y+0.5f, position.z);
    }
}