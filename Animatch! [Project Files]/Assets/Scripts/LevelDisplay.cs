using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelDisplay : MonoBehaviour
{
    public GameObject Level, Moves, BotPanel, TopPanel, LevelText, Arrow, PanelCol, congrats, complete;
    public GameObject CompleteScreen, LevelScreen, ExitTop, ExitBot;
    GameObject audioManager;

    GameObject lvl;
    int last=50;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        var audioScript = audioManager.GetComponent<AudioManager>();

        lvl = GameObject.FindGameObjectWithTag("LevelManager");
        GoStraight();

        Random.InitState((int)System.DateTime.Now.Ticks);
        Color col = Random.ColorHSV(0f, 1f, 0.3f, 0.6f, 1f, 1f);
        TopPanel.GetComponent<Image>().color = col;
        BotPanel.GetComponent<Image>().color = col;
        PanelCol.GetComponent<Image>().color = col;
        ExitBot.GetComponent<Image>().color = col;

        var myScript = lvl.GetComponent<LevelManager>();
        int level = myScript.GetLevel();
        Level.GetComponent<Text>().text = level.ToString();

        int moves = myScript.GetMoves();
        Moves.GetComponent<Text>().text = "It took you " + moves.ToString() + " moves!";

        if (level == 1 && myScript.GetMoves() == 0 || myScript.skipping == true)
        {
            myScript.skipping = false;
        }
        else
        {
            audioScript.CrossFade("bgm", "");
            audioScript.Play("win");
        }
        if (myScript.level==last) // auto exit for last level
        {
            ExitBot.SetActive(false);
            ExitTop.SetActive(false);
        }
    }

    public void GoToNext()
    {
        Arrow.SetActive(false);
        ExitBot.SetActive(false);
        ExitTop.SetActive(false);

        var myScript = lvl.GetComponent<LevelManager>();
        if (myScript.level != last)
        {
            Animation anim1, anim2;
            anim1 = CompleteScreen.GetComponent<Animation>();
            anim1.Play("Complete");
            anim2 = LevelScreen.GetComponent<Animation>();
            anim2.Play("NewLevel");
            StartCoroutine(DelayPlay());
            
            myScript.IncrementLevel();
            StartCoroutine(NextLevel(myScript.level));
        }
        else
        {
            Animation anim1, anim2;
            anim1 = CompleteScreen.GetComponent<Animation>();
            anim1.Play("Complete");
            anim2 = LevelScreen.GetComponent<Animation>();
            anim2.Play("NewLevel");

            PanelCol.SetActive(false);
            congrats.SetActive(true);
            complete.SetActive(true);

            myScript.IncrementLevel();
            StartCoroutine(EndGame());
        }
        if(!myScript.testing) // if not testing, update information of levels to be stored
        {
            if (myScript.maxLevel < myScript.level)
                myScript.maxLevel = myScript.level;
            if (myScript.maxMove[myScript.level - 1] > myScript.moves || myScript.maxMove[myScript.level - 1] == 0)
            {
                myScript.levelMove[myScript.level - 1] = myScript.moves;
                myScript.maxMove[myScript.level - 1] = myScript.moves;
            }
        }
        SaveLoad.SaveData();
        myScript.Load();
    }

    private void GoStraight() // directly enter a level after showing level number
    {
        var myScript = lvl.GetComponent<LevelManager>();
        int level = myScript.GetLevel();
        if (level == 1 && myScript.GetMoves() == 0 || myScript.skipping == true) // beginning of game or skipped
        {
            Arrow.SetActive(false);
            ExitBot.SetActive(false);
            ExitTop.SetActive(false);
            CompleteScreen.SetActive(false);

            Animation anim2;
            anim2 = LevelScreen.GetComponent<Animation>();
            anim2.Play("NewLevel");

            StartCoroutine(NextLevel(myScript.level));
        }
    }

    public void quitgame()
    {
        var myScript = lvl.GetComponent<LevelManager>();

        Animation anim1, anim2;
        anim1 = CompleteScreen.GetComponent<Animation>();
        anim1.Play("Complete");
        anim2 = LevelScreen.GetComponent<Animation>();
        anim2.Play("NewLevel");
        StartCoroutine(DelayPlay());

        myScript.IncrementLevel();

        if (!myScript.testing) // if not testing, update information of levels to be stored
        {
            if (myScript.maxLevel < myScript.level)
                myScript.maxLevel = myScript.level;
            if (myScript.maxMove[myScript.level - 1] > myScript.moves || myScript.maxMove[myScript.level - 1] == 0)
            {
                myScript.levelMove[myScript.level - 1] = myScript.moves;
                myScript.maxMove[myScript.level - 1] = myScript.moves;
            }
        }
        SaveLoad.SaveData();
        myScript.Load();
        SceneManager.LoadScene("AnimalPit");
    }

    IEnumerator NextLevel(int level)
    {
        LevelText.GetComponent<Text>().text = "LEVEL " + level.ToString();
        yield return new WaitForSeconds(3); // show next level name for 3 seconds
        SceneManager.LoadScene("GameScene");
    }

    IEnumerator EndGame()
    {
        LevelText.GetComponent<Text>().text = "";
        var audioScript = audioManager.GetComponent<AudioManager>();
        audioScript.Play("congrats");
        yield return new WaitForSeconds(4); // show "game completed" for 4 seconds
        SceneManager.LoadScene("AnimalPit");
    }

    IEnumerator DelayPlay()
    {
        yield return new WaitForSeconds(1.8f);
        var audioScript = audioManager.GetComponent<AudioManager>();
        audioScript.Play("cleared");
    }
}
