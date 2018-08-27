using System.Collections;
using UnityEngine;

public class Spinner : MonoBehaviour {
    public float speed;
    public float maxSpeed = 2f;
    public bool constantSpeed = false;
    public bool spinOnlyOnStart = false;
    public float durationOnStart = 1f;
    public bool isBonus = false;

    private int clockwise;

    private void Start() {
        if (!isBonus) {
            maxSpeed = GameController.instance.Score / 400f;
            maxSpeed = Mathf.Clamp(maxSpeed, 0f, 2.5f);
        }

        RotateRandomAngle();
        clockwise = Random.Range(0, 2) == 0 ? 1 : -1;
        if (constantSpeed) {
            speed = maxSpeed;
        } else {
            speed = Random.Range(0f, maxSpeed) * clockwise;
        }
        if (spinOnlyOnStart) {
            StartCoroutine(SpinOnce());
        }
    }

    private void Update() {
        if (!spinOnlyOnStart) {
            transform.Rotate(0, speed, 0);
        }
    }

    private IEnumerator SpinOnce() {
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

    public void StopSpin() {
        speed = Mathf.Lerp(speed, 0, Time.deltaTime);
    }
}
