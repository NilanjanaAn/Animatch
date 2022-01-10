using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bouncer : MonoBehaviour
{
    public GameObject top, panel;
    private GameObject[] chars;
    GameObject audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        var audioScript = audioManager.GetComponent<AudioManager>();
        audioScript.CrossFade("bgm", "complete");

        chars = GameObject.FindGameObjectsWithTag("Bounce");
        Random.InitState((int)System.DateTime.Now.Ticks);

        foreach (GameObject obj in chars)
        {
            obj.GetComponent<Rigidbody2D>().mass = Random.Range(1f, 3f);
            obj.GetComponent<Rigidbody2D>().gravityScale = Random.Range(15f, 25f);

            float x = Random.Range(-380f, 380f);
            float y = Random.Range(250f, 350f);
            Vector3 pos = new Vector3(x, y);
            obj.GetComponent<RectTransform>().position = pos;

            StartCoroutine(LockTop()); // put a lid to prevent animals from escaping xD
        }
    }

    IEnumerator LockTop()
    {
        yield return new WaitForSeconds(2f);
        top.SetActive(true);
    }

    public void ToStart()
    {
        SceneManager.LoadScene("StartScene");
    }
}
