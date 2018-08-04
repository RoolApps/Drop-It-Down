using UnityEngine;

public class ObstacleRemover : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
    }
}
