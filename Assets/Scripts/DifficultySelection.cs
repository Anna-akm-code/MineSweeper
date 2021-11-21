using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class DifficultySelection : MonoBehaviour
{
    ToggleGroup toggleGroup;

    List<Text> hiScores = new List<Text>();

    private void Awake()
    {
        hiScores.Add(GameObject.Find("HiScore - easy").GetComponent<Text>());
        hiScores.Add(GameObject.Find("HiScore - normal").GetComponent<Text>());
        hiScores.Add(GameObject.Find("HiScore - hard").GetComponent<Text>());
        toggleGroup = GetComponent<ToggleGroup>();
    }


    // Start is called before the first frame update
    void Start()
    {
        SetHiScores();
        
    }


    void SetHiScores()
    {
        int i = 1;
        foreach(Text text in hiScores)
        {
            string pref = "HighScore" + i.ToString();
            if (!PlayerPrefs.HasKey(pref))
            {
                PlayerPrefs.SetInt(pref, 9999);
            }
            text.text = PlayerPrefs.GetInt(pref).ToString();
            i++;
        }
    }

    public void StartGame()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        Debug.Log(toggle.name + " " + toggle.GetComponentInChildren<Text>().text);
        int a;
        switch (toggle.name)
        {
            case "Toggle - easy":
                a = 1;
                break;
            case "Toggle - normal":
                a = 2;
                break;
            case "Toggle - hard":
                a = 3;
                break;
            default:
                a = 1;
                break;
        }
        PlayerPrefs.SetInt("Difficulty", a);
        SceneManager.LoadScene("MinesweeperGame");
    }

    public void ExitGame()
    {
        Debug.Log("Game Exit");
        Application.Quit();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}




}
