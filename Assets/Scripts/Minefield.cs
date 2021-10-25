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
    List<int> victoryList = new List<int>();

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
                victoryList.Add(cells[i].index);
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


    public void LeftClick(int index)
    {
        Debug.Log("State = " + cells[index].state.ToString() + ", val = " + cells[index].value.ToString());
        Cell cell = cells[index];
        if (cell.state == Cell.State.Lock)
        {
            if (cell.value == -1)
            {
                cell.state = Cell.State.Explode;
                cell.UpdateSprite();
                Explode();
                //LOSS
            }
            else
            {
                OpenCell(cell, true);
            }
        }
    }

    public void RightClick(int index)
    {
        Debug.Log("State = " + cells[index].state.ToString() + ", val = " + cells[index].value.ToString());
        Cell cell = cells[index];
        if (cell.state == Cell.State.Lock)
        {
            cell.state = Cell.State.Flag;
            cell.UpdateSprite();
        }
        else if (cell.state == Cell.State.Flag)
        {
            cell.state = Cell.State.Lock;
            cell.UpdateSprite();
        }
    }

    void Explode()
    {
        foreach (Cell cell in cells)
        {
            if ((cell.state == Cell.State.Flag) | (cell.state == Cell.State.Lock))
            {
                OpenCell(cell);
            }
        }
    }
    void OpenCell(Cell cell, bool cascade = false)
    {
        cell.state = Cell.State.Open;
        cell.UpdateSprite();
        if ((cell.value == 0) & cascade)
        {
            Cascade(cell);
        }
        victoryList.Remove(cell.index);
        if (victoryList.Count == 0)
        {
            Debug.Log("VICTORY");
            //VICTORY
        }
    }
    void Cascade(Cell cell)
    {
        List<int> zeros = new List<int>();
        zeros.Add(cell.index);
        while (zeros.Count > 0)
        {
            foreach (int i in cells[zeros[0]].surroundings)
            {
                if (cells[i].state != Cell.State.Open)
                {
                    OpenCell(cells[i]);
                    if (cells[i].value == 0)
                    {
                        zeros.Add(i);
                    }
                }

            }
            zeros.RemoveAt(0);
        }

    }

}