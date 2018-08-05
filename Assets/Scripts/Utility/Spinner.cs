using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {
    public float duration = 1f;
    public float speed = 5f;
    public bool infinity = false;

    private void Start() {
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
}
