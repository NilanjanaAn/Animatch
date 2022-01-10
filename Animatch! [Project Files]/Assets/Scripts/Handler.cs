using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Handler : MonoBehaviour
{
    public List<Button> cells = new List<Button>();

    [SerializeField]
    private Sprite cover;

    public Sprite[] chars;
    public List<Sprite> animals = new List<Sprite>();

    private int correct;
    private int endCount;

    private bool first, second;
    private int firstInd, secondInd;
    private string firstName, secondName;

    public int moves;

    void Start()
    {
        moves = 0;
        GetCells();
        AddListeners();
        SelectSprites();
        Randomize(animals);
        endCount = cells.Count / 2;
    }
    
    void GetCells()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Cell");
        int k = 0;
        foreach (GameObject obj in objs)
        {
            cells.Add(obj.GetComponent<Button>());
            cells[k++].image.sprite = cover;
        }
    }

    void AddListeners() // add an OnClick functionality to each of the spawned buttons
    {
        foreach (Button cell in cells)
        {
            cell.onClick.AddListener(()=>Gameplay());
        }
    }

    public void Gameplay()
    {
        string name = EventSystem.current.currentSelectedGameObject.name; // the gameobject being clicked
        if(!first)
        {
            first = true;
            moves++;
            firstInd = int.Parse(name)-1;
            firstName = animals[firstInd].name;
            cells[firstInd].image.sprite = animals[firstInd];
            cells[firstInd].interactable = false;
        }
        else if(!second)
        {
            second = true;
            secondInd = int.Parse(name)-1;
            secondName = animals[secondInd].name;
            cells[secondInd].image.sprite = animals[secondInd];

            StartCoroutine(IsMatch());
        }
    }

    void Randomize(List<Sprite> images)
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        for (int i = 0; i < images.Count; i++)
        {
            Sprite temp =images[i];
            int r = UnityEngine.Random.Range(i, images.Count); // swap with any image after it
            images[i] = images[r];
            images[r] = temp;
        }
    }

    IEnumerator IsMatch()
    {
        if (firstName == secondName) // cards matched successfully
        {
            yield return new WaitForSeconds(.15f);
            correct++;
            // cannot click on those buttons again
            cells[firstInd].interactable = false;
            cells[secondInd].interactable = false;

            IsDone();
        }
        else // failed to match cards
        {
            yield return new WaitForSeconds(.5f);
            // flip both cards
            cells[firstInd].image.sprite = cover;
            cells[secondInd].image.sprite = cover;
            // make the first button clickable again
            cells[firstInd].interactable = true;
        }
        yield return new WaitForSeconds(.00015f);
        first = false;
        second = false;
    }

    void IsDone()
    {
        if(correct == endCount) // level completed
        {
            GameObject lvl = GameObject.FindGameObjectWithTag("LevelManager");
            var myScript = lvl.GetComponent<LevelManager>();
            myScript.moves = moves;
            SceneManager.LoadScene("LevelCompleteScene");
        }
    }

    void SelectSprites()
    {
        int count = cells.Count;
        int[] taken= new int[30];
        int[] selected = new int[30];
        Array.Clear(taken, 0, taken.Length);

        for (int i = 0; i < (count / 2); i++) 
        {
            int r;
            UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks); // random seed initalized with system time
            do // pick an animal not yet taken
            {
                r = UnityEngine.Random.Range(0, 30); // range is [0,30)
                if (taken[r] == 0)
                    break;
            } while (true);
            taken[r] = 1; // mark as taken
            selected[i] = r;
        }
        int index = 0;
        for (int i = 0; i < count; i++) // add selected animals to sprite list
        {
            if (index == (count/2))
                index = 0;
            animals.Add(chars[selected[index]]);
            index++;
            
        }
    }
}
