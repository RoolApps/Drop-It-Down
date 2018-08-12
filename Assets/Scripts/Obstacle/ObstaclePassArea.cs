using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePassArea : MonoBehaviour {
    private float destructionForse = 300f;
    private List<GameObject> obstacles = new List<GameObject>();

    private void Start() {
        foreach(Transform obstacle in transform.parent) {
            if (obstacle.CompareTag("Obstacle")) {
                obstacles.Add(obstacle.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("PlayerArea")) {
            destructObstacles();
        }
    }

    private void destructObstacles() {
        foreach (GameObject obstacle in obstacles) {
            Material material = obstacle.GetComponent<MeshRenderer>().material;
            Utility.MakeTransparenty(material);
            Utility.SetOpacity(material, 0.5f);

            Rigidbody rigidbody = obstacle.GetComponent<Rigidbody>();
            if (rigidbody) {
                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;
                rigidbody.AddExplosionForce(destructionForse, transform.position, 5f, .2f);
                obstacle.GetComponentsInChildren<MeshCollider>().ToList().ForEach(m => m.enabled = false);
            }
        }
    }
}
