using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour {
    public static GameController instance;

    public Text scoreText;
    public Text gameOverText;
    public TextMeshPro popupText;

    public int Score { get; private set; }

    private bool gameIsOver = false;
    private ObstacleCreator obstacleCreator;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }else if(instance != this) {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
        
        Time.timeScale = 1f;
        gameOverText.gameObject.SetActive(false);

        Bonus.Effects = BonusEffect.None;

        obstacleCreator = new ObstacleCreator();
        SpawnObstacle(3);
    }

    private void Update() {
        if (gameIsOver && Input.GetMouseButtonDown(0)) {
            ResumeGame();
        }
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
        Time.timeScale = 0f;
        gameOverText.gameObject.SetActive(true);
    }

    public void ResumeGame() {
        gameIsOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ColorSheme.instance.Generate();
    }

    public void SpawnObstacle(int count = 1) {
        for(int i = 0; i < count; i++) {
            obstacleCreator.CreateObstacle();
        }
    }
}
