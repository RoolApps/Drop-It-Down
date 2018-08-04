using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

    #region public variables
    public float destructionForse = 1f;
    public PassedArea passedArea;
    #endregion

    #region private variables
    private GameController gameController;
    private List<GameObject> obstacles = new List<GameObject>();
    #endregion

    #region private methods
    private void Start() {
        foreach(Transform obstacle in transform) {
            if (obstacle.CompareTag("Obstacle")) obstacles.Add(obstacle.gameObject);
        }

        if (passedArea) passedArea.passed += destruct;
        gameController = FindObjectOfType<GameController>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) destruct();
    }

    private void destruct() {
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
    #endregion
}
