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
        float shadeFactor = .6f;
        Color bottom = ColorSheme.instance.Current.background;
        bottom.r *= 1 - shadeFactor;
        bottom.g *= 1 - shadeFactor;
        bottom.b *= 1 - shadeFactor;
        colors[0] = bottom;
        colors[2] = bottom;
        mesh.colors = colors;
    }
}
