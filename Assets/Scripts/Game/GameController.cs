using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public int starReward = 5;
    public int bombReward = 10;
    public int magnetReward = 10;
    public float speed = 5f;
    public Text scoreText;
    public Text gameOverText;
    public GameObject popupText;

    private int score = 0;
    private Transform player;
    private bool gameIsOver = false;
    private bool magnetEffect = false;

    private void Start() {
        Time.timeScale = 1f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameOverText.gameObject.SetActive(false);
    }

    private void Update() {
        if (gameIsOver && Input.GetMouseButtonDown(0)) {
            ResumeGame();
        }

        if (magnetEffect) {
            GameObject[] stars = Physics.OverlapSphere(player.position, 8).Where(c => c.CompareTag("StarBonus")).Select(c => c.gameObject).ToArray();
            foreach (var star in stars) {
                star.transform.LookAt(player);
                star.transform.position = Vector3.MoveTowards(star.transform.position, player.position, .2f);
            }
        }

        Debug.Log(magnetEffect);
        Debug.Log(Time.time);
    }

    private void UpdateScoreText() {
        scoreText.text = score.ToString();
    }

    private void InstantiatePopupText(float text) {
        var txt = Instantiate(popupText, player.position + new Vector3(Random.Range(-2, 2), 0, -1), Quaternion.identity);
        txt.GetComponent<TextMesh>().text = "+" + text.ToString();
    }

    private void EncreaseSpeed(float value) {
        speed += value;
    }

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

    public void EncreaseScore(int value = 1) {
        score += value;

        InstantiatePopupText(value);

        UpdateScoreText();

        if (score % 10 == 0) {
            EncreaseSpeed(.2f);
        }
    }

    public void playerCollideWithObstacle() {
        EndGame();
    }

    #region Bonus Effects
    public void starBonusEffect() {
        EncreaseScore(starReward);
    }

    public void bombBonusEffect() {
        EncreaseScore(bombReward);
        ObstacleController[] obstacles = GameObject.FindObjectsOfType<ObstacleController>();
        foreach (var obstacle in obstacles) {
            obstacle.destruct();
        }
    }

    public void magnetBonusEffect() {
        EncreaseScore(magnetReward);
        StartCoroutine(StartMagnetBonusEffect());
    }

    private IEnumerator StartMagnetBonusEffect() {
        magnetEffect = true;
        yield return new WaitForSeconds(10);
        magnetEffect = false;
    }
    #endregion
}
