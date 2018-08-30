using UnityEngine;
using TMPro;

public class EndGameMenu : MonoBehaviour {

    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI currentScore;

    void Start () {
        bestScore.text = PlayerPrefs.GetInt("bestScore").ToString();
        currentScore.text = PlayerPrefs.GetInt("currentScore").ToString();
    }
}
