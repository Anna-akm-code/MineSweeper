using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineCounter : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Awake()
    {
        text = this.GetComponent<Text>();
    }

    public void UpdateText(int num)
    {
        text.text = num.ToString();
    }
}
