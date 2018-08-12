using UnityEngine;

public class ObstacleEndArea : MonoBehaviour {
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("PlayerArea")) {
            GameController.instance.SpawnObstacle();
        }
    }
}
