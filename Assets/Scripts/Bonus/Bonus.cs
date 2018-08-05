using UnityEngine;

public abstract class Bonus : MonoBehaviour {
    protected GameController gameController;

    private void Start() {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Destroy(gameObject);
            effect();
        }
    }

    abstract protected void effect();
}
