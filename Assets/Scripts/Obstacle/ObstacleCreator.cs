using System.Linq;
using UnityEngine;

public class ObstacleCreator : MonoBehaviour {
    private GameObject[] obstacles;

    private void Start () {
        obstacles = Resources.LoadAll("Obstacles").Select(o => (o as GameObject).gameObject).ToArray();
    }

    public void CreateObstacle() {
        Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform.position, Quaternion.identity);
    }
}

