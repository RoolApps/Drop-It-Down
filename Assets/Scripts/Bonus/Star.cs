using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    private GameController gameController;

    #region private methods
    private void Start() {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            gameController.EncreaseScore(gameController.speed);
            Destroy(gameObject);
        }
    }
    #endregion
}
