using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    private bool isColliding = false;

    private void Update() {
        isColliding = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (isColliding) return;
        isColliding = true;

        if (other.CompareTag("PlayerSphere")) {
            GameController.instance.EndGame();
        }
    }
}
