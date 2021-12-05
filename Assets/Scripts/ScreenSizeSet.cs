using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSizeSet : MonoBehaviour
{
    ToggleGroup toggleGroup;
    private void Awake()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        InitialScreenSize();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetScreenSize();
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    void InitialScreenSize()
    {
        if (!PlayerPrefs.HasKey("ScreenSize"))
        {
            PlayerPrefs.SetInt("ScreenSize", 1);
        }
    }

    public void ApplySize()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        int a;
        switch (toggle.name)
        {
            case "Toggle -x1":
                a = 1;
                break;
            case "Toggle -x2":
                a = 2;
                break;
            default:
                a = 1;
                break;
            }
        PlayerPrefs.SetInt("ScreenSize", a);
        SetScreenSize();
        }

    void SetScreenSize()
    {
        switch (PlayerPrefs.GetInt("ScreenSize"))
        {   
            case 2:
                Screen.SetResolution(1280, 720, false);
                FindObjectOfType<Camera>().GetComponent<Camera>().orthographicSize = 22.5f;
                break;
            default:
                Screen.SetResolution(640, 360, false);
                FindObjectOfType<Camera>().GetComponent<Camera>().orthographicSize = 11.25f;
                break;

        }
    }

}
