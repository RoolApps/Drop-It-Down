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

    public Pallete Current { get; private set; }

    private string[][] htmlShemes = new string[][] {
        new string[]{ "#032030", "#D0DFE6", "#F71E35", "#1794A5"},
        new string[]{ "#6abe83", "#f06966", "#dee2d1", "#f1ac9d"},
        new string[]{ "#102E37", "#F78D3F", "#2BBBD8", "#FCD271"},
        new string[]{ "#2C2C2C", "#FCFCFC", "#FF5F5F", "#83FFE6"},
        new string[]{ "#09194F", "#00818A", "#FDFDEB", "#F9CE00"},
        new string[]{ "#F75940", "#3DC7BE", "#364857", "#334252"},
        new string[]{ "#222831", "#FD7013", "#EEEEEE", "#393E46"},
        new string[]{ "#FCE38A", "#F38181", "#95E1D3", "#EAFFD0"},
        new string[]{ "#252A34", "#08D9D6", "#FF2E63", "#EAEAEA"},
        new string[]{ "#E8222D", "#F5E0A3", "#EEA282", "#200A3E"},
        new string[]{ "#50C1E9", "#F5F7FA", "#7A57D1", "#5BE7C4"},
        new string[]{ "#F0B775", "#D25565", "#FFFDC0", "#2E94B9"},
        new string[]{ "#0f0f0f", "#a39e9e", "#2d2d2d", "#bc8420"},
        new string[]{ "#FFE869", "#57D1C9", "#FFFBCB", "#ED5485"},
        new string[]{ "#dd0a35", "#014955", "#e4d1d3", "#1687a7"},
        new string[]{ "#005792", "#f6f6e9", "#fd5f00", "#13334c"},
        new string[]{ "#2d095c", "#dd7777", "#20366b", "#eae3e3"},
        new string[]{ "#eec60a", "#f07810", "#f5f4e8", "#c50d66"},
        new string[]{ "#060608", "#a696c8", "#2470a0", "#fad3cf"}
    };

    private List<Pallete> palletes = new List<Pallete>();

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
