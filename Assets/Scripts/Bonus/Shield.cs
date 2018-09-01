using UnityEngine;

public class Shield : Bonus {
    private void Start() {
        ColorUtility.TryParseHtmlString("#1ea4ff", out color);
    }

    protected override void SelfBonusEffect() {
        base.SelfBonusEffect();
        AudioController.instance.Play("ShieldEffect");
    }
}