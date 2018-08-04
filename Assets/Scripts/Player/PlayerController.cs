using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region public variables
    public float sensitivity = 5f;
    #endregion

    #region private variables
    private GameController gameController;
    #endregion

    #region private methods
    private void Start() {
        gameController = FindObjectOfType<GameController>();
    }

    private void Update() {
        transform.Rotate(0, -Input.GetAxis("Mouse X") * sensitivity, 0);
    }

    private void FixedUpdate() {
        transform.Translate(Vector3.down * gameController.speed * Time.deltaTime);
    }
    #endregion
}
