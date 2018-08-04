using UnityEngine;

public class Follower : MonoBehaviour {

    #region public variables
    public Transform _object;
    #endregion

    #region private methods   
    void Update () {
        transform.position = _object.position;
	}
    #endregion
}
