using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    #region private variables
    private GameController gameController;
    private bool isColliding = false;
    #endregion

    #region private methods
    private void Start() {
        gameController = FindObjectOfType<GameController>();
    }

    private void Update() {
        isColliding = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (isColliding) return;
        isColliding = true;

        if (other.CompareTag("Player")) {
            gameController.EndGame();
        }
    }
    #endregion
}
