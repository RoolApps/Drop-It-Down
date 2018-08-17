using System.Linq;
using UnityEngine;
using EZCameraShake;

public class Bomb : Bonus {
    protected override void SelfBonusEffect() {
        GameObject.FindObjectsOfType<ObstacleController>().ToList().ForEach(o => o.Boom());
        CameraShaker.Instance.ShakeOnce(2f, 1f, .1f, 1f);
    }
}
