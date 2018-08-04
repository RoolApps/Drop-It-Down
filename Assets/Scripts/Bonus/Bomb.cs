using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    #region private methods
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) Destroy(gameObject);
    }
    #endregion
}
