using System.Collections;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour {
    public GameObject menu;
    public GameObject settings;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI currentScore;
    public GameObject soundOnButton;
    public GameObject soundOffButton;
    public GameObject musicOnButton;
    public GameObject musicOffButton;

    private const int on = 1;
    private const int off = 0;

    private Animator animator;
    private bool pressed = false;

    private void Start() {
        animator = GetComponent<Animator>();

        menu.SetActive(true);
        settings.SetActive(false);

        bestScore.text = PlayerPrefs.GetInt("bestScore").ToString();
        currentScore.text = PlayerPrefs.GetInt("currentScore").ToString();

        if (PlayerPrefs.GetInt("Music") == on) {
            MusicOn();
        } else {
            MusicOff();
        }

        if (PlayerPrefs.GetInt("Sound") == on) {
            SoundOn();
        } else {
            SoundOff();
        }
    }

    #region public
    public void SettingsPressed() {
        pressed = !pressed;
        if (pressed) {
            animator.SetTrigger("SettingsOn");
        } else {
            animator.SetTrigger("SettingsOff");
        }
    }

    public void ExitApplication() {
        Application.Quit();
    }

    public void SoundOn() {
        PlayerPrefs.SetInt("Sound", on);
        soundOffButton.SetActive(false);
        soundOnButton.SetActive(true);
    }

    public void SoundOff() {
        PlayerPrefs.SetInt("Sound", off);
        soundOnButton.SetActive(false);
        soundOffButton.SetActive(true);
    }

    public void MusicOn() {
        PlayerPrefs.SetInt("Music", on);
        musicOnButton.SetActive(true);
        musicOffButton.SetActive(false);
        AudioController.instance.MusicOn();
    }

    public void MusicOff() {
        PlayerPrefs.SetInt("Music", off);
        musicOnButton.SetActive(false);
        musicOffButton.SetActive(true);
        AudioController.instance.MusicOff();
    }

    public void Like() {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.RoolApps.did");
    }
    #endregion
}
