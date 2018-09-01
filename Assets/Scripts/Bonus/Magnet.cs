using UnityEngine;

public class Magnet : Bonus {
    private void Start() { 
        ColorUtility.TryParseHtmlString("#cb1eff", out color);
    }

    protected override void SelfBonusEffect() {
        base.SelfBonusEffect();
        AudioController.instance.Play("MagnetEffect");
    }
}
