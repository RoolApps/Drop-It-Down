using UnityEngine;

public class Follower : MonoBehaviour {
    public Transform followTo;

    void Update () {
        transform.position = followTo.position;
	}
}
