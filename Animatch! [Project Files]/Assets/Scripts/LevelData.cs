using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData // a serializable container to store minimum moves for all levels and maximum level reached
{
    public int[] maxMove = new int[51]; // umm.. actually minMove
    public int maxlevel;

    public LevelData()
    {
        GameObject lvl = GameObject.FindGameObjectWithTag("LevelManager");
        var myScript = lvl.GetComponent<LevelManager>();
        maxMove = myScript.maxMove;
        maxlevel = myScript.maxLevel;
    }
}
