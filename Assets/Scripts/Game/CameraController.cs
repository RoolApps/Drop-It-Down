using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform followTo;

    private Vector3 offset;

    void Start () {
        offset = transform.position - followTo.transform.position;
	}
	
	void LateUpdate () {
        transform.position = followTo.transform.position + offset;
    }
}
