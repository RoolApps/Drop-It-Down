using UnityEngine;

public class Manager : MonoBehaviour {
    public GameObject gameController;
    public GameObject colorSheme;

	private void Awake () {
		if(GameController.instance == null) {
            Instantiate(gameController);
        }

        if(ColorSheme.instance == null) {
            Instantiate(colorSheme);
        }
	}
}
