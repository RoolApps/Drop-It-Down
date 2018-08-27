using UnityEngine;
using UnityEngine.UI;

public class BonusActivator : MonoBehaviour {
    public BonusEffect type;

    private RawImage image;
    private bool activated = false;

    private void Start() {
        image = GetComponent<RawImage>();
        image.color = Color.gray;
        image.enabled = false;
    }

    private void Update () {
        if (activated) return;
        if(Bonus.BonusAccessibility(type) <= GameController.instance.Score) {
            image.enabled = true;
            image.color = Color.white;

            GetComponent<Animator>().SetTrigger("Animate");
            activated = true;

            foreach(var bonus in GameObject.FindObjectsOfType<BonusSpawner>()) {
                if (bonus.SpawnActivatedBonus(type)) break;
            }
        }
	}
}
