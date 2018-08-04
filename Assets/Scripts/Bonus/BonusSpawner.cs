using System.Collections;
using System.Linq;
using UnityEngine;

public class BonusSpawner : MonoBehaviour {

    #region public variables
    public float chance = 10f;
    #endregion

    #region private variables
    private GameObject[] bonuses;
    #endregion

    #region private methods
    private void Start () {
        bonuses = Resources.LoadAll("Bonuses").Select(o => (o as GameObject).gameObject).ToArray();
        if (Enumerable.Range(0, (int)(chance / 10f)).Contains(Random.Range(0, 10))) spawn();
	}

    private void spawn() {
        Instantiate(bonuses[Random.Range(0, bonuses.Length)], transform.position, transform.rotation, transform);
    }
    #endregion
}
