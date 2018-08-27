using System.Linq;
using UnityEngine;
using EZCameraShake;

public class Bomb : Bonus {
    private void Start() {
        ColorUtility.TryParseHtmlString("#000", out color);
    }

    protected override void SelfBonusEffect() {
        base.SelfBonusEffect();
        GameObject.FindObjectsOfType<ObstacleController>().ToList().ForEach(o => o.Boom());
        CameraShaker.Instance.ShakeOnce(3f, 1.5f, .1f, 2f);
    }
}
