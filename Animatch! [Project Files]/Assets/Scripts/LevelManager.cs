using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int level = 1, moves = 0;
    public bool sound = true, mod = false, skipping = false, testing = false;
    public int[] levelMove = new int[51];
    public int[] maxMove = new int[51];
    public int maxLevel = 1;

    void Awake()
    {
        for (int i = 0; i < 51; i++)
        {
            levelMove[i] = 0;
            maxMove[i] = 0;
        }

        Load();

        // allow only one instance to exist in one scene and avoid duplicates
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void IncrementLevel()
    {
        level++;
    }

    public int GetLevel()
    {
        return level;
    }
    
    public int GetMoves()
    {
        return moves;
    }

    public void Save()
    {
        if(!testing)
            SaveLoad.SaveData();
    }

    public void Load()
    {
        if(!testing)
        {
            LevelData data = SaveLoad.LoadData();
            if (data != null)
            {
                maxLevel = data.maxlevel;
                maxMove = data.maxMove;
            }
            Debug.Log("New load, maxlevel = " + maxLevel);
        }
    }
}
