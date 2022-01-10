using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayGUI : MonoBehaviour
{
    public GameObject menuB, exitB, sound, panel, levelText, soundB, ExitButton, SoundButton;
    public GameObject menu, quitScreen;
    public Sprite soundON, soundOFF, menuSym, crossSym;
    int level;
    bool showing = false;
    GameObject lvl;

    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        Color32 color = Random.ColorHSV(0.5f, 1f, 0.3f, 0.6f, 1f, 1f);
        panel.GetComponent<Image>().color = color;
        menuB.GetComponent<Image>().color = color;
        soundB.GetComponent<Image>().color = color;
        exitB.GetComponent<Image>().color = color;

        lvl = GameObject.FindGameObjectWithTag("LevelManager");
        var myScript = lvl.GetComponent<LevelManager>();
        level = myScript.GetLevel();
        levelText.GetComponent<Text>().text = "Level  "+level.ToString();
        
        if (myScript.sound == true)
            sound.GetComponent<Image>().sprite = soundON;
        else
            sound.GetComponent<Image>().sprite = soundOFF;
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

    public void Menu()
    {
        Animation anim1, anim2;
        anim1 = SoundButton.GetComponent<Animation>();
        anim2 = ExitButton.GetComponent<Animation>();
        if(showing==false)
        {
            StartCoroutine(ChangeSprite(crossSym,.51f));
            anim1.Play("SoundShow");
            anim2.Play("ExitShow");
            showing = true;
        }
        else
        {
            StartCoroutine(ChangeSprite(menuSym,.75f));
            anim1.Play("SoundHide");
            anim2.Play("ExitHide");
            showing = false;
        }
    }

    public void Exit()
    {
        quitScreen.SetActive(true);
    }

    public void No()
    {
        quitScreen.SetActive(false);
    }

    public void Yes()
    {
        SceneManager.LoadScene("AnimalPit");
    }

    IEnumerator ChangeSprite(Sprite s, float t)
    {
        yield return new WaitForSeconds(t);
        menu.GetComponent<Image>().sprite = s;
    }
}
