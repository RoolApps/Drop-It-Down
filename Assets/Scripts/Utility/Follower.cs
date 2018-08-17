using UnityEngine;

public class Follower : MonoBehaviour {
    public Transform followTo;

    private Vector3 offset;

    void Start() {
        offset = transform.position - followTo.transform.position;
    }

    void LateUpdate() {
        transform.position = followTo.transform.position + offset;
    }
}
