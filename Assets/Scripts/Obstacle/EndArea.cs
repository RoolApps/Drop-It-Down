using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndArea : MonoBehaviour {
    private ObstacleCreator obstacleCreator;

    private void Start() {
        obstacleCreator = GameObject.FindObjectOfType<ObstacleCreator>();
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            obstacleCreator.CreateObstacle();
        }
    }
}
