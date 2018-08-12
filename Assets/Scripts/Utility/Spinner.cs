using System.Collections;
using UnityEngine;

public class Spinner : MonoBehaviour {
    public float speed = 5f;
    public float duration = 1f;
    public bool infinity = false;

    private void Start() {
        RotateRandomAngle();
        if (!infinity) {
            StartCoroutine(spinOnce());
        }
    }

    private void Update() {
        if (!infinity) return;
        transform.Rotate(0, speed, 0);
    }

    private IEnumerator spinOnce() {
        float elapsed = .0f;
        while(elapsed < duration) {
            elapsed += Time.deltaTime;
            transform.Rotate(0, speed, 0);
            yield return null;
        }
    }

    private void RotateRandomAngle() {
        transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
    }
}
