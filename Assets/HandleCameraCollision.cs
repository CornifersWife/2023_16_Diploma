using System;
using UnityEngine;

public class HandleCameraCollision : MonoBehaviour {
    private bool isColliding;
    public bool IsColliding => isColliding;

    private void OnTriggerEnter(Collider other) {
        isColliding = true;
    }

    private void OnTriggerExit(Collider other) {
        isColliding = false;
    }
}
