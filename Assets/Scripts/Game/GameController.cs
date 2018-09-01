using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour {
    public static GameController instance;

    public Text scoreText;
    public TextMeshPro popupText;
    public GameObject endGameMenu;

    public int Score { get; private set; }

    private bool gameIsOver = false;
    private ObstacleCreator obstacleCreator;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }

        Time.timeScale = 0f;
        endGameMenu.SetActive(true);

        BonusActivator.spawned = 0;

        Bonus.Effects = BonusEffect.None;
        Bonus.ShuffleBonusOrder();

        obstacleCreator = new ObstacleCreator();
        SpawnObstacle(3);
    }

    public void EncreaseScore(int value, Color color) {
        Score += value;
        scoreText.text = Score.ToString();

        InstantiatePopupText(value.ToString(), color);
    }

    private void InstantiatePopupText(string text, Color color) {
        Vector3 random = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
        Vector3 position = PlayerController.instance.transform.position + random;
        TextMeshPro txt = Instantiate(popupText, new Vector3(position.x, position.y, -3), Quaternion.identity);
        txt.color = color;
        txt.text = "+" + text;
    }

    public void EndGame() {
        if (gameIsOver) return;
        gameIsOver = true;

        AudioController.instance.Play("GameOver");

        PlayerPrefs.SetInt("currentScore", Score);
        int bestScore = PlayerPrefs.GetInt("bestScore");
        if(Score > bestScore) {
            PlayerPrefs.SetInt("bestScore", Score);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ColorSheme.instance.Generate();
    }

    public void StartGame() {
        gameIsOver = false;
        Time.timeScale = 1f;
        endGameMenu.SetActive(false);
    }

    public void SpawnObstacle(int count = 1) {
        for(int i = 0; i < count; i++) {
            obstacleCreator.CreateObstacle();
        }
    }
}
