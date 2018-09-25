using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController instance;

    public Text scoreText;    
    public TextMeshPro popupText;
    public GameObject UIMenu;
    public ParticleSystem explode;

    public int Score { get; private set; }

    private bool gameIsOver = false;
    private bool gameIsPaused = false;
    private ObstacleCreator obstacleCreator;

    private void Start() {
        if (instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }

        Time.timeScale = 0f;
        UIMenu.SetActive(true);

        BonusActivator.spawned = 0;

        Bonus.Effects = BonusEffect.None;
        Bonus.ShuffleBonusOrder();

        obstacleCreator = new ObstacleCreator();
    }

    private void InstantiatePopupText(string text, Color color) {
        Vector3 random = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
        Vector3 position = PlayerController.instance.transform.position + random;
        TextMeshPro txt = Instantiate(popupText, new Vector3(position.x, position.y, -3), Quaternion.identity);
        txt.color = color;
        txt.text = "+" + text;
    }

    private IEnumerator EndGameCouroutine() {
        AudioController.instance.Play("GameOver");
        PlayerController player = PlayerController.instance;
        player.normalSpeed = 0f;

        player.gameObject.SetActive(false);
        Instantiate(explode, player.transform.position, Quaternion.identity);
        GameObject.FindObjectsOfType<ObstacleController>().ToList().ForEach(o => o.Boom());

        yield return new WaitForSeconds(1f);

        Ads ads = Ads.instance;
        int gamesPlayed = PlayerPrefs.GetInt("GamesPlayed") + 1;
        if (gamesPlayed % ads.gamesCount == 0) {
            ads.Show();
            gamesPlayed = 0;
        }
        PlayerPrefs.SetInt("GamesPlayed", gamesPlayed);

        PlayerPrefs.SetInt("currentScore", Score);
        int bestScore = PlayerPrefs.GetInt("bestScore");
        if (Score > bestScore) {
            PlayerPrefs.SetInt("bestScore", Score);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ColorSheme.instance.Generate();
    }

    public void EncreaseScore(int value, Color color) {
        Score += value;
        scoreText.text = Score.ToString();

        InstantiatePopupText(value.ToString(), color);
    }

    public void EndGame() {
        if (gameIsOver) return;
        gameIsOver = true;

        StartCoroutine(EndGameCouroutine());
    }

    public void StartGame(string s) {
        gameIsOver = false;
        Time.timeScale = 1f;
        UIMenu.SetActive(false);


        float ps = 0f;
        float ss = 0f;
        switch (s) {
            case "Easy": {
                break;
            }
            case "Normal": {
                ps = .5f;
                ss = .5f;
                break;
            }
            case "Hard": {
                ps = 1f;
                ss = 1f;
                break;
            }
            default: break;
        }

        PlayerPrefs.SetFloat("difficultyPlayerSpeed", ps);
        PlayerPrefs.SetFloat("difficultySpinSpeed", ss);

        SpawnObstacle(3);
    }

    public void PauseGame() {
        if (!gameIsPaused) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
        gameIsPaused = !gameIsPaused;
    }

    public void SpawnObstacle(int count = 1) {
        for(int i = 0; i < count; i++) {
            obstacleCreator.CreateObstacle();
        }
    }
}
