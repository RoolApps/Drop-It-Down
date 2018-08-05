using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {
    public PassedArea passedArea;
    public float destructionForse = 1f;

    private GameController gameController;
    private List<GameObject> obstacles = new List<GameObject>();

    private void Start() {
        gameController = GameObject.FindObjectOfType<GameController>();

        foreach(Transform obstacle in transform) {
            if (obstacle.CompareTag("Obstacle")) {
                obstacles.Add(obstacle.gameObject);
            }
        }

        if (passedArea) {
            passedArea.passed += destruct;
        }
    }

    public void destruct() {
        gameController.EncreaseScore();
        foreach(GameObject obstacle in obstacles) {
            Rigidbody rb = obstacle.GetComponent<Rigidbody>();
            if (rb) {
                rb.useGravity = true;
                obstacle.GetComponent<MeshCollider>().isTrigger = false;
                rb.AddExplosionForce(destructionForse, transform.position, 1f, 1f);
            }
        }
    }
}
