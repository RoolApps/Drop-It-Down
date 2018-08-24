using UnityEngine;

public enum BonusType { Star, Magnet, Bomb, Boost, Shield, TimeDelay};

public abstract class Bonus : MonoBehaviour {
    public int reward = 0;

    public BonusType Type { get; protected set; }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PlayerSphere")) {
            GameController.instance.EncreaseScore(reward);
            PlayerController.instance.BonusCollected(this);
            SelfBonusEffect();
            Destroy(gameObject);
        }
    }

    static public int BonusAccessibility(BonusType type) {
        switch (type) {
            case BonusType.Star: {
                return 0;
            }
            case BonusType.Magnet: {
                return 50;
            }
            case BonusType.Bomb: {
                return 100;
            }
            case BonusType.Boost: {
                return 200;
            }
            case BonusType.Shield: {
                return 400;
            }
            case BonusType.TimeDelay: {
                return 500;
            }
            default: return 0;
        }
    }

    abstract protected void SelfBonusEffect();
}
