using System.Linq;
using UnityEngine;

[System.Serializable]
public class Sound {
    public string tag;
    public AudioClip clip;

    [Range(0, 1)]
    public float volume = 1f;
    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
}

public class AudioController : MonoBehaviour {
    public static AudioController instance;

    public Sound[] sounds;

    private void Start() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (instance != this) {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

        if (PlayerPrefs.GetInt("Music") == 1) {
            MusicOn();
        }
    }

    public void Play(string tag) {
        Sound sound = sounds.First(s => s.tag == tag);
        if (sound != null && PlayerPrefs.GetInt("Sound") == 1) {
            sound.source.Play();
        }
    }

    public void Stop(string tag) {
        Sound sound = sounds.First(s => s.tag == tag);
        if (sound != null) {
            sound.source.Stop();
        }
    }

    public void MusicOn() {
        sounds.First(s => s.tag == "Background").source.Play();
    }

    public void MusicOff() {
        sounds.First(s => s.tag == "Background").source.Stop();
    }
}
