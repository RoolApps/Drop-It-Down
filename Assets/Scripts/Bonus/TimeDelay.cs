using UnityEngine;

public class TimeDelay : Bonus {
    private void Start() {
        ColorUtility.TryParseHtmlString("#ffa01e", out color);
    }

    protected override void SelfBonusEffect() {
        base.SelfBonusEffect();
        AudioController.instance.Play("TimeDelayEffect");
    }
}
