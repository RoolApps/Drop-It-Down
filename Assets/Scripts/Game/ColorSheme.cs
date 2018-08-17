using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallete {
    public Pallete(Color p, Color c, Color o, Color b) {
        player = p;
        cylinder = c;
        obstacle = o;
        background = b;
    }

    public Color player;
    public Color cylinder;
    public Color obstacle;
    public Color background;
}

public class ColorSheme : MonoBehaviour {
    public static ColorSheme instance;

    private string[][] htmlShemes = new string[][] {
        new string[]{ "#5198C3", "#C4E457", "#CB4DB3", "#E9A859"},
        new string[]{ "#FE3F44", "#644AD8", "#FFDA40", "#42E73A"}
    };

    private List<Pallete> palletes = new List<Pallete>();

    public Pallete Current { get; private set; }

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        palletes = htmlShemes.Select(c => ToPallete(c)).ToList();

        Generate();
    }

    public void Generate() {
        Current = RandomPallete();
    }

    private Pallete RandomPallete() {
        return palletes[Random.Range(0, palletes.Count)];
    }

    private Pallete ToPallete(string[] sheme) {
        Color playerColor;
        Color cylinderColor;
        Color obstacleColor;
        Color backgroundColor;

        ColorUtility.TryParseHtmlString(sheme[0], out playerColor);
        ColorUtility.TryParseHtmlString(sheme[1], out cylinderColor);
        ColorUtility.TryParseHtmlString(sheme[2], out obstacleColor);
        ColorUtility.TryParseHtmlString(sheme[3], out backgroundColor);

        return new Pallete(playerColor, cylinderColor, obstacleColor, backgroundColor);
    }
}
