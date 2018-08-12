using UnityEngine;

public class ObstacleCylinder : MonoBehaviour {
    private Material material;
    private float minOpacity = .5f;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PlayerArea")) {
            material = GetComponent<MeshRenderer>().material;
            Utility.MakeTransparenty(material);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("PlayerArea")) {
            float distance = Vector3.Distance(other.transform.position, transform.position);
            float opacity = minOpacity + (distance / 10);
            Utility.SetOpacity(material, opacity);
        }
    }
}
