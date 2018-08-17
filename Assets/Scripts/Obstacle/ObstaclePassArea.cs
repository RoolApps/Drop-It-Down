using UnityEngine;

public class ObstaclePassArea : MonoBehaviour {
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("PlayerArea")) {
            Utility.FindFirstParentWithTag(gameObject, "ObstacleController").GetComponent<ObstacleController>().Boom(transform);
        }
    }
}
