using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassedArea : MonoBehaviour {
    public delegate void PassedDelegate();
    public event PassedDelegate passed;

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            passed();
        }
    }
}
