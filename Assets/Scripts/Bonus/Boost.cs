using UnityEngine;

public class Boost : Bonus {
    private void Start() {
        ColorUtility.TryParseHtmlString("#1efff6", out color);
    }

    protected override void SelfBonusEffect() {
        base.SelfBonusEffect();
        AudioController.instance.Play("BoostEffect");
    }
}
