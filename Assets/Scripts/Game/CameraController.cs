using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    #region public variables
    public Transform _object;
    #endregion

    #region private variables
    private Vector3 offset;
    #endregion

    #region private variables
    void Start () {
        offset = transform.position - _object.transform.position;
	}
	
	void LateUpdate () {
        transform.position = _object.transform.position + offset;
    }
    #endregion
}
