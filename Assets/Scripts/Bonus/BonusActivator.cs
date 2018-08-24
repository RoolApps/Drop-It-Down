using UnityEngine;
using UnityEngine.UI;

public class BonusActivator : MonoBehaviour {
    public BonusType type;

    private RawImage image;
    private bool activated = false;

    private void Start() {
        image = GetComponent<RawImage>();
        image.color = Color.gray;
        image.enabled = false;
    }

    private void Update () {
        if (activated) return;
        if(GameController.instance.Score >= Bonus.BonusAccessibility(type)) {
            image.enabled = true;
            image.color = Color.white;

            GetComponent<Animator>().SetTrigger("Animate");
            activated = true;
        }
	}
}
