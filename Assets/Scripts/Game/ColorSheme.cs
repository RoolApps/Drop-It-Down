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
        new string[]{ "#7acfd6", "#f1f0ee", "#b11a21", "#e0474c"},
        //new string[]{ "", "", "", ""},
        //new string[]{ "#737495", "#68a8ad", "#f17d80", "#c4d4af"},
        //new string[]{ "#b86c99", "#fce8d8", "#d78ab7", "#ffffff"},
        //new string[]{ "#7F95D1", "#FFC0BE", "#FF82A9", "#FFEBE7"},
        //new string[]{ "#19405e", "#febebc", "#fafaf9", "#3279ad"},
        //new string[]{ "#CCCC99", "#9999CC", "#FFFFCC", "#99CC99"},
        //new string[]{ "#060608", "#a696c8", "#2470a0", "#fad3cf"},
        //new string[]{ "#c50d66", "#D25565", "#2E94B9", "#F0B775"},
        //new string[]{ "#08D9D6", "#FF2E63", "#252A34", "#EAEAEA"},
        //new string[]{ "#102E37", "#F78D3F", "#FCD271", "#2BBBD8"},
        //new string[]{ "#D74B4B", "#DCDDD8", "#475F77", "#354B5E"},
        //new string[]{ "#E95D22", "#DF7782", "#D9CCB9", "#017890"},
        //new string[]{ "#9BD7D5", "#FFF5C3", "#505050", "#FF7260"},
        //new string[]{ "#FF82A9", "#FFEBE7", "#FFC0BE", "#7F95D1"},
        //new string[]{ "#668b8a", "#9fb083", "#a47c64", "#f9eed3"},
        //new string[]{ "#4695d6", "#fed95c", "#fa6e57", "#f69e53"},
        //new string[]{ "#47b8e0", "#ff7473", "#34314c", "#ffc952"},
        //new string[]{ "#feee7d", "#ef5285", "#60c5ba", "#a5dff9"},
        //new string[]{ "#06565b", "#66a4ac", "#003a44", "#c2dde4"},
        //new string[]{ "#606c70", "#ddbb93", "#b38766", "#ccccbf"},
        //new string[]{ "#68a8ad", "#c4d4af", "#f17d80", "#737495"},
        //new string[]{ "#fbd14b", "#dedcee", "#6a60a9", "#fffcf0"},
        //new string[]{ "#57BE85", "#FFFFFF", "#D87575", "#7BCED7"},
        //new string[]{ "#E73A38", "#F7E4C5", "#445252", "#F8937E"},
        //new string[]{ "#9CC5C9", "#CDB599", "#D5544F", "#A08689"},
        //new string[]{ "#03a6ff", "#a3daff", "#0080ff", "#1ec0ff"},
        //new string[]{ "#03a6ff", "#e1982f", "#31364d", "#207ba1"},
        //new string[]{ "#E8CA78", "#E7E5DF", "#B1E0D8", "#393E41"},
        //new string[]{ "#00d2f1", "#00b796", "#cc0063", "#86269b"},
        //new string[]{ "#E5BD47", "#2C363F", "#2F6665", "#DCDCDD"},
        //new string[]{ "#cc5856", "#444a5b", "#78a4a1", "#dfaf6a"},
        //new string[]{ "#20938b", "#f3cc6f", "#de7921", "#69af86"},
        //new string[]{ "#E2001A", "#494747", "#F2E6E6", "#21A8A3"},
        //new string[]{ "#C08497", "#FFCAD4", "#B0D0D3", "#F7AF9D"},
        //new string[]{ "#ffb5ba", "#baddd6", "#61bfbe", "#4abbf3"},
        //new string[]{ "#c12127", "#CF9B61", "#5B6049", "#7A5930"},
        //new string[]{ "#FFD966", "#6DD0F2", "#363A42", "#F59ABE"},
        //new string[]{ "#005995", "#4ABDAC", "#FC4A1A", "#F7B733"},
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
        Current = palletes[0];
        //Current = RandomPallete();
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
