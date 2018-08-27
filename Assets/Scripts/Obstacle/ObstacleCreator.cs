using System.Linq;
using UnityEngine;

public class ObstacleCreator {
    private float minSpace = 1f;
    private float maxSpace = 5f;
    private GameObject[] obstaclePrefabs;
    private Vector3 position = Vector3.zero;

    public ObstacleCreator() {
        obstaclePrefabs = Resources.LoadAll("Obstacles").Select(o => (o as GameObject).gameObject).ToArray();
    }

    private GameObject NextRandomPrefab() {
        return obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
    }

    public void CreateObstacle() {
        //GameObject rnd = obstaclePrefabs[0];
        GameObject rnd = NextRandomPrefab();
        GameObject.Instantiate(rnd, position, Quaternion.identity);
        CapsuleCollider capsule = rnd.GetComponentInChildren<CapsuleCollider>();
        float height = 0f;
        if (capsule) height = capsule.height;
        position.y -= height + Random.Range(minSpace, maxSpace);
    }
}

