using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ObstaclePassArea : MonoBehaviour {
    private float destructionForse = 500f;
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
            StartCoroutine(OpacityDown(material));

            Rigidbody rigidbody = obstacle.GetComponent<Rigidbody>();
            if (rigidbody) {
                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;
                rigidbody.AddExplosionForce(destructionForse, transform.position, 0f, 0f);
                obstacle.GetComponentsInChildren<MeshCollider>().ToList().ForEach(m => m.enabled = false);
            }
        }
    }

    private IEnumerator OpacityDown(Material mat) {
        while (mat.color.a > 0) {
            Utility.SetOpacity(mat, mat.color.a - Time.deltaTime);
            yield return null;
        }
    }
}
