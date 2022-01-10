using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelColorPit : MonoBehaviour
{
    public GameObject panelf;

    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        Color32 color = Random.ColorHSV(0.2f, 0.8f, 0.5f, 0.7f, 1f, 1f);
        panelf.GetComponent<Image>().color = color;
    }
}
