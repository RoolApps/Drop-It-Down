using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassedArea : MonoBehaviour {

    #region public
    public delegate void PassedDelegate();
    public event PassedDelegate passed;
    #endregion

    #region private methods
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) passed();
    }
    #endregion
}
