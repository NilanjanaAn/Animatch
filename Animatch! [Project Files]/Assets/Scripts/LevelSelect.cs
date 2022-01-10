using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject Img, Moves, MovesText, LevelNum;
    public Sprite lockSym, openSym;

    int gotolevel;

    void Awake()
    {
        int.TryParse(this.name, out gotolevel); // read numerical name of the gameobject into an integer
        LevelNum.GetComponent<Text>().text = this.name;

        GameObject lvl = GameObject.FindGameObjectWithTag("LevelManager");
        var myScript = lvl.GetComponent<LevelManager>();
        myScript.testing = false;

        if ((myScript.maxMove[gotolevel] != 0) || (gotolevel == myScript.maxLevel)) // don't touch locked levels
        {
            GetComponent<Button>().interactable = true;
            if(gotolevel == myScript.maxLevel) // for the last unlocked level, show open lock
                Img.GetComponent<Image>().sprite = openSym;
            else if (myScript.maxMove[gotolevel] != 0) // for already cleared levels, show moves
            {
                Img.GetComponent<Image>().color = new Color32(243, 182, 8, 255);
                Img.GetComponent<Image>().sprite = null;
                MovesText.SetActive(true);
                Moves.GetComponent<Text>().text = myScript.maxMove[gotolevel].ToString();
                Moves.SetActive(true);
            }
        }

    }

    public void SkipTo()
    {
        GameObject lvl = GameObject.FindGameObjectWithTag("LevelManager");
        var myScript = lvl.GetComponent<LevelManager>();

        int.TryParse(this.name, out gotolevel); // read numerical name of the gameobject into an integer
        myScript.level = gotolevel; // skip to that level
        myScript.skipping = true;

        SceneManager.LoadScene("LevelCompleteScene");
    }
}
