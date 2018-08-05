using UnityEngine;

public class Remover : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
    }
}
