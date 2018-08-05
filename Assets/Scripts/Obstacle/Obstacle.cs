using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    private bool isColliding = false;
    private GameController gameController;

    private void Start() {
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    private void Update() {
        isColliding = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (isColliding) return;
        isColliding = true;

        if (other.CompareTag("Player")) {
            gameController.playerCollideWithObstacle();
        }
    }
}
