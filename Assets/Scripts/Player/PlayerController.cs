using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float sensitivity = 1.5f;
    public float speed = 5f;

    private bool pressed = false;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) pressed = true;
        if (Input.GetMouseButtonUp(0)) pressed = false;
    }

    private void FixedUpdate() {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void LateUpdate() {
        if (pressed) {
            transform.Rotate(0, -Input.GetAxis("Mouse X") * sensitivity, 0);
        }
    }

    public void EncreaseSpeed(float delta = .2f) {
        speed += delta;
    }
}
