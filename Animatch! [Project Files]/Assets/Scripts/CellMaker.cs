using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellMaker : MonoBehaviour
{
    public Transform Grid;
    public GameObject cell;
    public List<Sprite> bgs = new List<Sprite>();

    public int row, col;
    private int level;
    GameObject audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        var audioScript = audioManager.GetComponent<AudioManager>();
        audioScript.CrossFade("start", "bgm");

        Grid.GetComponent<Image>().sprite = bgs[Random.Range(0, 10)];

        GameObject lvl = GameObject.FindGameObjectWithTag("LevelManager");
        var myScript = lvl.GetComponent<LevelManager>();
        level = myScript.GetLevel();

        if (level < 5)
        {
            row = 3;
            col = 4;
        }
        else if (level < 10)
        {
            row = 4;
            col = 4;
        }
        else if (level < 20)
        {
            row = 4;
            col = 5;
        }
        else if (level < 25)
        {
            row = 4;
            col = 6;
        }
        else if (level < 30)
        {
            row = 5;
            col = 6;
        }
        else if (level < 40)
        {
            row = 6;
            col = 6;
        }
        else if (level < 45)
        {
            row = 6;
            col = 7;
        }
        else if (level <= 50)
        {
            row = 6;
            col = 8;
        }
        makeGrid(row, col);
    }

    void makeGrid(int row, int col)
    {
        var gridLayout = Grid.GetComponent<GridLayoutGroup>();
        gridLayout.constraintCount = row;

        for (int i = 0; i < (row * col); i++)
        {
            GameObject newCell = Instantiate(cell);
            newCell.name = "" + (i + 1);
            newCell.transform.SetParent(Grid, false);

            Vector2 size;
            if (level >= 30)
                size = new Vector2(35, 35);
            else if(level>=25)
            {
                size = new Vector2(45, 45);
            }
            else if (level >= 10)
                size = new Vector2(50, 50);
            else
                size = new Vector2(60, 60);
            Grid.GetComponent<GridLayoutGroup>().cellSize = size;
        }
    }
}
