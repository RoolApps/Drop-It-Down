using System.Collections;
using UnityEngine;

public class Spinner : MonoBehaviour {
    public float maxSpeed = 2f;
    public bool constantSpeed = false;
    public bool spinOnlyOnStart = false;
    public float durationOnStart = 1f;

    private int clockwise;
    private float speed;

    private void Start() {
        clockwise = Random.Range(0, 2) == 0 ? 1 : -1;
        if (constantSpeed) {
            speed = maxSpeed;
        } else {
            speed = Random.Range(.1f, maxSpeed) * clockwise;
        }
        RotateRandomAngle();
        if (spinOnlyOnStart) {
            StartCoroutine(spinOnce());
        }
    }

    private void Update() {
        if (!spinOnlyOnStart) {
            transform.Rotate(0, speed, 0);
        }
    }

    private IEnumerator spinOnce() {
        float elapsed = .0f;
        while(elapsed < durationOnStart) {
            elapsed += Time.deltaTime;
            transform.Rotate(0, speed, 0);
            yield return null;
        }
    }

    private void RotateRandomAngle() {
        transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
    }
}
