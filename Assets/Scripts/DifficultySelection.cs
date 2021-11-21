using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class DifficultySelection : MonoBehaviour
{
    ToggleGroup toggleGroup;
    // Start is called before the first frame update
    void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
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
