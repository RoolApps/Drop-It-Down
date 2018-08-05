using System.Collections;
using System.Linq;
using UnityEngine;

public class BonusSpawner : MonoBehaviour {
    public int chance = 10;
    public GameObject[] prefabs;

    private GameObject[] bonuses;

    private void Start () {
        if (prefabs.Length == 0) {
            bonuses = Resources.LoadAll("Bonuses").Select(o => (o as GameObject).gameObject).ToArray();
        } else {
            bonuses = prefabs;
        }

        if (Random.Range(0, 100) <= chance) {
            spawnBonus();
        }
	}

    private void spawnBonus() {
        Instantiate(bonuses[Random.Range(0, bonuses.Length)], transform.position, transform.rotation, transform);
    }
}
