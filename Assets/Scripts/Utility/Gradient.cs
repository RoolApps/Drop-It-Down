using UnityEngine;

public class Gradient : MonoBehaviour {
    private void Start() {
        var mesh = GetComponent<MeshFilter>().mesh;
        var colors = new Color[mesh.vertices.Length];
        // top
        Color top = ColorSheme.instance.Current.background;
        colors[0] = top;
        colors[2] = top;
        // bottom
        Color bottom = Utility.Darker(ColorSheme.instance.Current.background);
        colors[1] = bottom;
        colors[3] = bottom;
        mesh.colors = colors;
    }
}
