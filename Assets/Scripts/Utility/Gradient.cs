using UnityEngine;

public class Gradient : MonoBehaviour {
    private void Start() {
        var mesh = GetComponent<MeshFilter>().mesh;
        var colors = new Color[mesh.vertices.Length];
        // top
        Color top = ColorSheme.instance.Current.background;
        colors[1] = top;
        colors[3] = top;
        // bottom
        Color bottom = Utility.Darker(ColorSheme.instance.Current.background);
        colors[0] = bottom;
        colors[2] = bottom;
        mesh.colors = colors;
    }
}
