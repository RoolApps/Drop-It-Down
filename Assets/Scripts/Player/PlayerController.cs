using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;

    public float speed = 5f;
    public float sensitivity = 2f;

    private bool pressed = false;
    private bool magnetEffect = false;
    private Vector2 startTouchPosition;
    private List<GameObject> spheres = new List<GameObject>();

    private void Start() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        spheres = Utility.FindChildrenWithTag(gameObject, "PlayerSphere");
        spheres.ForEach(s => Utility.SetColor(Utility.GetMaterial(s), ColorSheme.instance.Current.player));
    }

    private void Update() {
        if (magnetEffect) {
            GameObject[] stars = Physics.OverlapSphere(transform.position, 8).Where(c => c.CompareTag("StarBonus")).Select(c => c.gameObject).ToArray();
            foreach (var star in stars) {
                Vector3 position = spheres[Random.Range(0, spheres.Count)].transform.position;
                star.transform.position = Vector3.MoveTowards(star.transform.position, position, .2f);
            }
        }
    }

    private void FixedUpdate() {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void LateUpdate() {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) pressed = true;
        if (Input.GetMouseButtonUp(0)) pressed = false;
        if (pressed) {
            transform.Rotate(Vector3.up * -Input.GetAxis("Mouse X") * sensitivity);
        }
#else
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase) {
                case TouchPhase.Began: {
                    startTouchPosition = touch.position;
                    break;
                }
                case TouchPhase.Moved: {
                    Vector2 direction = touch.position - startTouchPosition;
                    float angle = -direction.x / 10f;
                    transform.Rotate(Vector3.up * angle * sensitivity);
                    startTouchPosition = touch.position;
                    break;
                }
                default: break;
            }
        }
#endif
    }

    private IEnumerator MagnetEffect() {
        magnetEffect = true;
        yield return new WaitForSeconds(5);
        magnetEffect = false;
    }

    public void EncreaseSpeed(float delta = .2f) {
        speed += delta;
    }

    public void BonusCollected(Bonus bonus) {
        if(bonus is Magnet) {
            StartCoroutine(MagnetEffect());
        }
    }
}
