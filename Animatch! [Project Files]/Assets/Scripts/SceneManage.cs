using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class SceneManage : MonoBehaviour
{
    public GameObject panel, info, about, soundb, sound, secret, skip, right, wrong, warn;
    public Sprite secretSym, skipSym, soundON, soundOFF;
    public GameObject aboutScreen, helpScreen, secretScreen, skipScreen, quitScreen;
    public InputField pass, skiplevel;

    string scene;
    GameObject lvl, audioManager;
    Color32 col1, col2;

    void Start()
    {
        lvl = GameObject.FindGameObjectWithTag("LevelManager");
        var myScript = lvl.GetComponent<LevelManager>();
        myScript.level = 1;
        myScript.moves = 0;

        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        var audioScript = audioManager.GetComponent<AudioManager>();
        Sound s = Array.Find(audioScript.sounds, item => item.name == "start");
        if (s != null)
        {
            if (!s.source.isPlaying)
                audioScript.CrossFade("complete", "start");
        }

        scene = SceneManager.GetActiveScene().name;

        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        col1 = UnityEngine.Random.ColorHSV(0f, 0.48f, 0.4f, 0.8f, 1f, 1f);
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        col2 = UnityEngine.Random.ColorHSV(0.52f, 1f, 0.4f, 0.8f, 1f, 1f);

        if (myScript.sound == true)
            sound.GetComponent<Image>().sprite = soundON;
        else
            sound.GetComponent<Image>().sprite = soundOFF;

        if (myScript.mod == true)
            skip.GetComponent<Image>().sprite = skipSym;
        else
            skip.GetComponent<Image>().sprite = secretSym;
    }

    public void Play()
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void Exit()
    {
        Time.timeScale = 0;
        quitScreen.SetActive(true);
    }

    public void No()
    {
        Time.timeScale = 1;
        quitScreen.SetActive(false);
    }

    public void Yes()
    {
        Application.Quit();
    }

    public void Update()
    {
        panel.GetComponent<Image>().color = Color.LerpUnclamped(col1, col2, Mathf.PingPong(Time.time / 2.5f, 1f));
        about.GetComponent<Image>().color = Color.LerpUnclamped(col1, col2, Mathf.PingPong(Time.time / 2.5f, 1f));
        info.GetComponent<Image>().color = Color.LerpUnclamped(col1, col2, Mathf.PingPong(Time.time / 2.5f, 1f));
        soundb.GetComponent<Image>().color = Color.LerpUnclamped(col1, col2, Mathf.PingPong(Time.time / 2.5f, 1f));
        secret.GetComponent<Image>().color = Color.LerpUnclamped(col1, col2, Mathf.PingPong(Time.time / 2.5f, 1f));
    }

    public void Authenticate()
    {
        if (string.Compare(pass.text.ToString(), "matchmod") == 0) // password
        {
            var myScript = lvl.GetComponent<LevelManager>();
            myScript.mod = true;
            skip.GetComponent<Image>().sprite = skipSym;
            right.SetActive(true);
            wrong.SetActive(false);
            StartCoroutine(GoToSkip());
        }
        else
        {
            wrong.SetActive(true);
            pass.Select();
        }
    }

    public void SkipTo()
    {
        int gotolevel;
        if (int.TryParse(skiplevel.text.ToString(), out gotolevel)) // do not change until a number has been input
        {
            if (gotolevel > 50)
                gotolevel = 50;
            else if (gotolevel < 1)
                gotolevel = 1;
            var myScript = lvl.GetComponent<LevelManager>();
            myScript.level = gotolevel;
            myScript.skipping = true;
            myScript.testing = true;
            warn.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene("LevelCompleteScene");
        }
        else
        {
            warn.SetActive(true);
            skiplevel.Select();
        }
    }

    public void Soundsystem()
    {
        if (sound.GetComponent<Image>().sprite == soundON)
            TurnSoundOff();
        else
            TurnSoundOn();
    }

    void TurnSoundOn()
    {
        sound.GetComponent<Image>().sprite = soundON;
        var myScript = lvl.GetComponent<LevelManager>();
        myScript.sound = true;
    }

    void TurnSoundOff()
    {
        sound.GetComponent<Image>().sprite = soundOFF;
        var myScript = lvl.GetComponent<LevelManager>();
        myScript.sound = false;
    }

    public void SecretSkipShow()
    {
        Time.timeScale = 0;
        if (skip.GetComponent<Image>().sprite == secretSym)
        {
            secretScreen.SetActive(true);
            pass.Select();
        }
        else
        {
            skipScreen.SetActive(true);
            skiplevel.Select();
        }
    }

    public void SecretHide()
    {
        right.SetActive(false);
        wrong.SetActive(false);
        pass.Select();
        pass.text = "";
        Time.timeScale = 1;
        secretScreen.SetActive(false);
    }

    public void SkipHide()
    {
        Time.timeScale = 1;
        skiplevel.Select();
        skiplevel.text = "";
        warn.SetActive(false);
        skipScreen.SetActive(false);
    }

    public void AboutShow()
    {
        Time.timeScale = 0;
        aboutScreen.SetActive(true);
    }

    public void AboutHide()
    {
        Time.timeScale = 1;
        aboutScreen.SetActive(false);
    }
    public void HelpShow()
    {
        Time.timeScale = 0;
        helpScreen.SetActive(true);
    }

    public void HelpHide()
    {
        Time.timeScale = 1;
        helpScreen.SetActive(false);
    }

    IEnumerator GoToSkip()
    {
        yield return new WaitForSecondsRealtime(0.75f);
        secretScreen.SetActive(false);
        skipScreen.SetActive(true);
        skiplevel.Select();
    }
}
