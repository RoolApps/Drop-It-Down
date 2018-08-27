using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility {
    static public void MakeTransparenty(Material material) {
        material.SetFloat("_Mode", 2);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }

    static public void SetOpacity(Material material, float value) {
        Color color = material.color;
        color.a = value;
        material.color = color;
    }

    static public void SetColor(Material material, Color color) {
        material.color = color;
    }

    static public Color Darker(Color c) {
        Color color = c;
        float shadeFactor = .6f;
        color.r *= shadeFactor;
        color.g *= shadeFactor;
        color.b *= shadeFactor;
        return color;
    }

    static public Material GetMaterial(GameObject target) {
        return target.GetComponent<MeshRenderer>().material;
    }

    static public GameObject FindFirstWithTag(GameObject target, string tag) {
        foreach (Transform t in target.transform) {
            if (t.CompareTag(tag)) {
                return t.gameObject;
            }
        }
        return null;
    }

    static public GameObject FindFirstParentWithTag(GameObject target, string tag) {
        Transform parent = target.transform.parent;
        while (parent) {
            if (parent.CompareTag(tag)) {
                return parent.gameObject;
            }
            parent = parent.parent;
        }
        return null;
    }

    static public List<GameObject> FindChildrenWithTag(GameObject target, string tag) {
        List<GameObject> objects = new List<GameObject>();
        foreach(Transform t in target.transform) {
            if (t.CompareTag(tag)) {
                objects.Add(t.gameObject);
            }
        }
        return objects;
    }

    static public List<GameObject> FindAllChildrenWithTag(GameObject target, string tag) {
        List<GameObject> objects = new List<GameObject>();
        foreach (Transform t in target.transform) {
            if (t.CompareTag(tag)) {
                objects.Add(t.gameObject);
            }
            objects.AddRange(FindAllChildrenWithTag(t.gameObject, tag));
        }
        return objects;
    }
}
