using UnityEngine;

public class ObstacleCylinder : MonoBehaviour {
    private Material material;
    private float minOpacity = .3f;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PlayerArea")) {
            material = GetComponent<MeshRenderer>().material;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("PlayerArea")) {
            Utility.SetOpacity(material, Mathf.Lerp(material.color.a, minOpacity, Time.deltaTime));
        }
    }
}
