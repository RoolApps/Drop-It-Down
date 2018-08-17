using UnityEngine;

public abstract class Bonus : MonoBehaviour {
    public int reward = 0;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PlayerSphere")) {
            GameController.instance.EncreaseScore(reward);
            PlayerController.instance.BonusCollected(this);
            SelfBonusEffect();
            Destroy(gameObject);
        }
    }

    abstract protected void SelfBonusEffect();
}
