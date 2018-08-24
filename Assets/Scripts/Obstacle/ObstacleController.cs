using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {
    private GameObject cylinder;
    private float destructionForse = 500f;
    private List<GameObject> obstacles = new List<GameObject>();
    private List<GameObject> destroyedObstacles = new List<GameObject>();
	
	private void Start () {
        obstacles.Clear();
        destroyedObstacles.Clear();
        obstacles = Utility.FindAllChildrenWithTag(gameObject, "Obstacle");
        foreach(GameObject obstacle in obstacles) {
            Utility.SetColor(Utility.GetMaterial(obstacle), ColorSheme.instance.Current.obstacle);
        }
        cylinder = Utility.FindFirstWithTag(gameObject, "Cylinder");
        Utility.SetColor(Utility.GetMaterial(cylinder), ColorSheme.instance.Current.cylinder);

        transform.Rotate(Vector3.up * Random.Range(0, 360));
    }

    public void Boom() {
        DestructObstacles(null);
    }

    public void Boom(Transform t) {
        DestructObstacles(t);
    }

    private void DestructObstacles(Transform t) {
        obstacles = obstacles.Except(destroyedObstacles).ToList();
        IEnumerable<GameObject> _obstacles = obstacles.Where(o => o.transform.position.y > t.position.y).Select(o => o);
        foreach (GameObject obstacle in (t != null ? _obstacles : obstacles)) {
            StartCoroutine(DestroyObstacle(obstacle));

            Rigidbody rigidbody = obstacle.GetComponent<Rigidbody>();
            if (rigidbody) {
                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;
                Vector3 explosionPosition = new Vector3(0, obstacle.transform.position.y, 0);
                rigidbody.AddExplosionForce(destructionForse, explosionPosition, 0f, 0.1f);
                obstacle.GetComponentsInChildren<MeshCollider>().ToList().ForEach(m => m.enabled = false);
            }
        }
    }

    private IEnumerator DestroyObstacle(GameObject target) {
        destroyedObstacles.Add(target);
        yield return new WaitForSeconds(1);
        Destroy(target);
    }
}
