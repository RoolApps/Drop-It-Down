using System.Linq;
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

    public Pallete Current { get; private set; }

    private string[][] htmlShemes = new string[][] {
        new string[]{ "#737495", "#68a8ad", "#f17d80", "#c4d4af"},
        new string[]{ "#b86c99", "#fce8d8", "#d78ab7", "#ffffff"},
        new string[]{ "#FFCC00", "#febebc", "#fafaf9", "#3279ad"},
        new string[]{ "#1A2828", "#9999CC", "#FFFFCC", "#99CC99"},
        new string[]{ "#060608", "#a696c8", "#2470a0", "#fad3cf"},
        new string[]{ "#c50d66", "#D25565", "#2E94B9", "#F0B775"},
        new string[]{ "#102E37", "#F78D3F", "#FCD271", "#2BBBD8"},
        new string[]{ "#D74B4B", "#DCDDD8", "#475F77", "#354B5E"},
        new string[]{ "#E95D22", "#DF7782", "#D9CCB9", "#017890"},
        new string[]{ "#9BD7D5", "#FFF5C3", "#505050", "#FF7260"},
        new string[]{ "#FF82A9", "#FFEBE7", "#FFC0BE", "#7F95D1"},
        new string[]{ "#feee7d", "#ef5285", "#60c5ba", "#a5dff9"},
        new string[]{ "#de5842", "#66a4ac", "#003a44", "#c2dde4"},
        new string[]{ "#000000", "#ddbb93", "#b38766", "#ccccbf"},
        new string[]{ "#68a8ad", "#c4d4af", "#f17d80", "#737495"},
        new string[]{ "#57BE85", "#FFFFFF", "#D87575", "#7BCED7"},
        new string[]{ "#E73A38", "#F7E4C5", "#445252", "#F8937E"},
        new string[]{ "#9CC5C9", "#CDB599", "#D5544F", "#A08689"},
        new string[]{ "#E5BD47", "#24a8ac", "#2F6665", "#DCDCDD"},
        new string[]{ "#cc5856", "#444a5b", "#78a4a1", "#dfaf6a"},
        new string[]{ "#20938b", "#f3cc6f", "#de7921", "#69af86"},
        new string[]{ "#E2001A", "#494747", "#F2E6E6", "#21A8A3"},
        new string[]{ "#ffb5ba", "#baddd6", "#61bfbe", "#4abbf3"},
        new string[]{ "#FFD966", "#6DD0F2", "#363A42", "#F59ABE"}
    };

    private List<Pallete> palletes = new List<Pallete>();

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);

            palletes = htmlShemes.Select(c => ToPallete(c)).ToList();
            Generate();
        } 
    }

    public void Generate() {
        //Current = palletes[0];
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
