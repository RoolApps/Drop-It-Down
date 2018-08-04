using System.IO;
using System.Linq;

using UnityEngine;
using UnityEditor;
using System.Collections;

public class ObstacleCreator : MonoBehaviour {

    #region private variables
    private GameObject[] obstacles;
    #endregion

    #region public methods
    public void CreateObstacle() {
        Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform.position, Quaternion.identity);
    }
    #endregion

    #region private methods
    private void Start () {
        obstacles = Resources.LoadAll("Obstacles").Select(o => (o as GameObject).gameObject).ToArray();
    }
    #endregion
}

