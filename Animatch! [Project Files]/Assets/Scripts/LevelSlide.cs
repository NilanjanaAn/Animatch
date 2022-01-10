using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSlide : MonoBehaviour
{
    private void Awake()
    {
        GameObject lvl = GameObject.FindGameObjectWithTag("LevelManager");
        var myScript = lvl.GetComponent<LevelManager>();
        myScript.Load();
    }

    public void next()
    {
        Animation slide;
        slide = GetComponent<Animation>();
        slide.Play("SlideLeft");
    }

    public void prev()
    {
        Animation slide;
        slide = GetComponent<Animation>();
        slide.Play("SlideRight");
    }

    public void back()
    {
        SceneManager.LoadScene("StartScene");
    }
}
