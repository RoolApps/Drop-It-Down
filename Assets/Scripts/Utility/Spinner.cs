using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

    #region public variables
    public float duration = 1f;
    public float maxSpeed = 5f;
    #endregion

    #region private methods
    private float speed;
    #endregion

    #region private methods
    private void Start() {
        speed = Random.Range(maxSpeed / 2, maxSpeed) * (Random.Range(0, 2) * 2 - 1);
        StartCoroutine(spinOnce());
    }

    private IEnumerator spinOnce() {
        float elapsed = .0f;
        while(duration != -1 ? elapsed < duration : true) {
            elapsed += Time.deltaTime;
            transform.Rotate(0, speed, 0);
            yield return null;
        }
    }
    #endregion
}
