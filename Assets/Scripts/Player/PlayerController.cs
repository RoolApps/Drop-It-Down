using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;

    public float speed = 5f;
    public float sensitivity = 2f;
    public ParticleSystem boostPs;
    public ParticleSystem magnetPs;

    public bool Shielded { get; private set; }

    private bool pressed = false;
    private bool magnetEffect = false;
    private bool boostEffect = false;
    private bool timeDelayEffect = false;
    private Vector2 startTouchPosition;
    private List<GameObject> spheres = new List<GameObject>();

    private int magnedEffectTime = 5;
    private int boostEffectTime = 5;
    private int shieldEffectTime = 5;
    private int timeDelayEffectTime = 5;

    private void Start() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        spheres = Utility.FindChildrenWithTag(gameObject, "PlayerSphere");
        spheres.ForEach(s => Utility.SetColor(Utility.GetMaterial(s), ColorSheme.instance.Current.player));

        boostPs.Stop();
        magnetPs.Stop();
    }

    private void Update() {
        if (magnetEffect) {
            GameObject[] stars = Physics.OverlapSphere(transform.position, 8).Where(c => c.GetComponent<Star>() != null).Select(c => c.gameObject).ToArray();
            foreach (var star in stars) {
                Vector3 position = spheres[Random.Range(0, spheres.Count)].transform.position;
                star.transform.position = Vector3.MoveTowards(star.transform.position, position, .25f);
            }
        }

        if (timeDelayEffect) {
            Spinner[] spinners = GameObject.FindObjectsOfType<Spinner>();
            foreach(var spinner in spinners) {
                spinner.speed = 0f;
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
        magnetPs.Play();
        yield return new WaitForSeconds(magnedEffectTime);
        magnetPs.Stop();
        magnetEffect = false;
    }

    private IEnumerator BoostEffect() {
        boostEffect = true;
        speed += 3;
        boostPs.Play();
        yield return new WaitForSeconds(boostEffectTime);
        speed -= 3;
        boostPs.Stop();
        boostEffect = false;
    }

    private IEnumerator ShieldEffect() {
        Shielded = true;
        yield return new WaitForSeconds(shieldEffectTime);
        Shielded = false;
    }

    private IEnumerator TimeDelayEffect() {
        timeDelayEffect = true;
        yield return new WaitForSeconds(timeDelayEffectTime);
        timeDelayEffect = false;
    }

    public void BonusCollected(Bonus bonus) {
        switch (bonus.Type) {
            case BonusType.Magnet: {
                if (magnetEffect) StopCoroutine("MagnetEffect");
                StartCoroutine("MagnetEffect");
                break;
            }
            case BonusType.Boost: {
                if (boostEffect) {
                    StopCoroutine("BoostEffect");
                    speed -= 3;
                }
                StartCoroutine("BoostEffect");
                break;
            }
            case BonusType.Shield: {
                if (Shielded) StopCoroutine("ShieldEffect");
                StartCoroutine("ShieldEffect");
                break;
            }
            case BonusType.TimeDelay: {
                if (timeDelayEffect) StopCoroutine("TimeDelayEffect");
                StartCoroutine("TimeDelayEffect");
                break;
            }
            default: break;
        }
    }
}
