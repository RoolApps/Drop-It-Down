using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;

    public float boostSpeed = 8f;
    public float normalSpeed = 5f;
    public float sensitivity = 2f;
    public ParticleSystem boostPs;
    public ParticleSystem magnetPs;
    public ParticleSystem shieldPs;
    public ParticleSystem timeDelayPs;

    private float speed;
    private bool pressed = false;
    private Vector2 startTouchPosition;
    private List<GameObject> spheres = new List<GameObject>();
    private Dictionary<BonusEffect, Effect> effects = new Dictionary<BonusEffect, Effect>() {
        { BonusEffect.Magnet, new Effect() },
        { BonusEffect.Boost, new Effect() },
        { BonusEffect.Shield, new Effect() },
        { BonusEffect.TimeDelay, new Effect() }
    };

    private class Effect {
        private ParticleSystem system;

        public float Time { get; set; }
        public ParticleSystem System {
            get {
                return system;
            }
            set {
                system = value;
                system.Stop();
            }
        }
    }

    private void Start() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        speed = normalSpeed;

        spheres = Utility.FindChildrenWithTag(gameObject, "PlayerSphere");
        spheres.ForEach(s => Utility.SetColor(Utility.GetMaterial(s), ColorSheme.instance.Current.player));

        effects[BonusEffect.Magnet].System = magnetPs;
        effects[BonusEffect.Boost].System = boostPs;
        effects[BonusEffect.Shield].System = shieldPs;
        effects[BonusEffect.TimeDelay].System = timeDelayPs;        
    }

    private void Update() {
        if (Bonus.HasEffect(BonusEffect.Magnet)) {
            GameObject[] stars = Physics.OverlapSphere(transform.position, 8).Where(c => c.GetComponent<Star>() != null).Select(c => c.gameObject).ToArray();
            foreach (var star in stars) {
                Vector3 position = spheres[Random.Range(0, spheres.Count)].transform.position;
                star.transform.position = Vector3.MoveTowards(star.transform.position, position, .25f);
            }
        }

        if (Bonus.HasEffect(BonusEffect.TimeDelay)) {
            Spinner[] spinners = GameObject.FindObjectsOfType<Spinner>();
            foreach(var spinner in spinners) {
                spinner.StopSpin();
            }
        }

        if (!Bonus.HasEffect(BonusEffect.Boost)) {
            speed = normalSpeed + (GameController.instance.Score / 1000f) + PlayerPrefs.GetFloat("difficultyPlayerSpeed");
        } else {
            speed = boostSpeed;
        }

        speed = Mathf.Clamp(speed, 0f, boostSpeed);
    }

    private void BoostScore() {
        GameController.instance.EncreaseScore(1, Color.cyan);
    }

    private void FixedUpdate() {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void LateUpdate() {
        if (Time.timeScale == 0f) return;
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

    private IEnumerator StartEffect(BonusEffect type) {
        while(effects[type].Time > 0f) {
            effects[type].Time -= Time.deltaTime;            
            yield return null;
        }
        effects[type].Time = 0f;
        effects[type].System.Stop();
        Bonus.RemoveEffect(type);
        if (type == BonusEffect.Boost) {
            CancelInvoke("BoostScore");
        }
    }

    public void BonusCollected(Bonus bonus) {
        BonusEffect type = bonus.type;
        if (effects.ContainsKey(type)) {
            bool startCoroutine = false;
            if (effects[type].Time == 0f) {
                startCoroutine = true;
                if(type == BonusEffect.Boost) {
                    InvokeRepeating("BoostScore", 0f, .3f);
                }
            }

            effects[type].Time += bonus.effectTime;

            ParticleSystem ps = effects[type].System;
            ps.Stop();
            var main = ps.main;
            main.duration = effects[type].Time;
            ps.Play();

            if (startCoroutine) {
                StartCoroutine(StartEffect(type));
            }
        }
    }
}
