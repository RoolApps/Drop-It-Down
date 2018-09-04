using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour {
    public static Ads instance;

    private string androidId = "2773380";

    public int gamesCount = 3;

    [SerializeField]
    private bool testMode = true;

    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);

            PlayerPrefs.SetInt("GamesPlayed", 0);

            Advertisement.Initialize(androidId, testMode);
        } else if(instance != this) {
            Destroy(gameObject);
        }
    }

    public bool Show() {
        if (Advertisement.IsReady()) {
            Advertisement.Show();
            return true;
        }
        return false;
    }
}
