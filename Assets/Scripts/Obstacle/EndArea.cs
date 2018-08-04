using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndArea : MonoBehaviour {

    #region private variables
    private ObstacleCreator[] obstacleCreators;
    #endregion

    #region private methods
    private void Start() {
        obstacleCreators = FindObjectsOfType<ObstacleCreator>();
    }
    #endregion

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            foreach(ObstacleCreator creator in obstacleCreators) creator.CreateObstacle();
        }
    }
}
