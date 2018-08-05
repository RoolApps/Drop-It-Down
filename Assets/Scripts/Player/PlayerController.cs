using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float sensitivity = 1.5f;

    private bool pressed = false;
    private GameController gameController;

    private void Start() {
        gameController = FindObjectOfType<GameController>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) pressed = true;
        if (Input.GetMouseButtonUp(0)) pressed = false;
    }

    private void FixedUpdate() {
        transform.Translate(Vector3.down * gameController.speed * Time.deltaTime);
        if (pressed) transform.Rotate(0, -Input.GetAxis("Mouse X") * sensitivity, 0);
    }
}
