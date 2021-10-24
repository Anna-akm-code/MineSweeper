using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Minefield : MonoBehaviour
{
    const int FieldWidth = 10;
    const int FieldHeight = 10;
    const int MinesNum = 20;

    [SerializeField]
    GameObject prefabCell;

    List<Cell> cells = new List<Cell>();
    List<GameObject> cellObjs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        GenerateCellObjects(FieldWidth, FieldHeight);
        Debug.Log("objects generated");
        GenerateMines(MinesNum, FieldWidth, FieldHeight);
        Debug.Log("mines generated");
        GenerateValues();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    void GenerateCellObjects(int width = FieldWidth, int height = FieldHeight)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Vector3 Location = new Vector3(j - 5, i - 5, -Camera.main.transform.position.z);
                cellObjs.Add(Instantiate(prefabCell) as GameObject);
                cellObjs.Last().transform.position = Location;
                cells.Add(cellObjs.Last().GetComponent<Cell>());
                cells.Last().surroundings = GetSurroundings(j, i, width, height);
                cells.Last().index = XYtoIndex(j, i);
            }
        }
    }

    List<int> GetSurroundings(int x, int y, int width, int height)
    {
        List<int> result = new List<int>();
        List<int> scopeX = new List<int>();
        scopeX.Add(x);
        if (x > 0)
        {
            scopeX.Add(x - 1);
        }
        if (x < (width - 1))
        {
            scopeX.Add(x + 1);
        }
        List<int> scopeY = new List<int>();
        scopeY.Add(y);
        if (y > 0)
        {
            scopeY.Add(y - 1);
        }
        if (y < (height - 1))
        {
            scopeY.Add(y + 1);
        }
        foreach (int i in scopeY)
        {
            foreach (int j in scopeX)
            {
                if (!((i == y) & (j == x)))
                {
                    result.Add(XYtoIndex(j, i, width));
                }
            }
        }
        return result;

    }

    int XYtoIndex(int x, int y, int width = FieldWidth)
    {
        return ((y * width) + x);
    }

    void GenerateMines(int minenum = MinesNum, int width = FieldWidth, int height = FieldHeight)
    {
        for (int i = 0; i < minenum; i++)
        {
            int place = Random.Range(0, cells.Count);
            while (CountSurroundingMines(place) == cells[place].surroundings.Count)
            {
                place = Random.Range(0, cells.Count);
            }
            cells[place].value = -1;
        }
    }

    void GenerateValues()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].value != -1)
            {
                cells[i].value = CountSurroundingMines(i);
            }
        }
    }

    int CountSurroundingMines(int index)
    {
        int result = 0;
        foreach (int i in cells[index].surroundings)
        {
            if (cells[i].value == -1)
            {
                result++;
            }
        }
        return result;
    }

   

}