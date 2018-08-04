using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    #region public variables
    public float speed = initialSpeed;

    public Text scoreText;
    public Text gameOverText;
    public GameObject popupText;
    #endregion

    #region private variables
    private float score = 0;
    private bool gameIsOver = false;
    private const float initialSpeed = 5f;

    private Transform player;
    #endregion

    #region private methods
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Time.timeScale = 1f;
        //speed = initialSpeed;
        gameOverText.gameObject.SetActive(false);
    }

    private void Update() {
        if (gameIsOver && Input.GetMouseButtonDown(0)) ResumeGame();
    }

    private void UpdateScoreText() {
        scoreText.text = score.ToString();
    }
    #endregion

    #region public methods
    public void EndGame() {
        if (gameIsOver) return;
        gameIsOver = true;
        Time.timeScale = 0f;
        gameOverText.gameObject.SetActive(true);
    }

    public void ResumeGame() {
        gameIsOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EncreaseScore(float value = 1f) {
        score += value;

        Instantiate(popupText, player.position, Quaternion.identity);
        popupText.GetComponent<TextMesh>().text = "+" + value.ToString();

        UpdateScoreText();

        if (score % 10 == 0) speed += 1f;
    }
    #endregion
}
