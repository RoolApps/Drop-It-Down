using System.Linq;
using UnityEngine;
using EZCameraShake;

public class Bomb : Bonus {
    private void Start() {
        Type = BonusType.Bomb;
    }

    protected override void SelfBonusEffect() {
        GameObject.FindObjectsOfType<ObstacleController>().ToList().ForEach(o => o.Boom());
        CameraShaker.Instance.ShakeOnce(3f, 1.5f, .1f, 2f);
    }
}
