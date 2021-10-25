using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //Sprites
    [SerializeField]
    Sprite spriteLock;
    [SerializeField]
    Sprite spriteMine;
    [SerializeField]
    Sprite spriteFlag;
    [SerializeField]
    Sprite spriteExplode;
    [SerializeField]
    List<Sprite> spritesVal;

    //State prop
    public enum State
    {
        Lock,
        Open,
        Flag,
        Explode
    }
    public State state = State.Lock;

    //GameObject's sprite renderer, assigned in Start
    SpriteRenderer sprite;

    //Minefield script, assigned in start
    public Minefield minefield;

    //cell's index in the list, assigned at creation
    public int index;

    //Cell's value, -1=mine
    public int value = 0;

    //Surrounding cells indexes
    public List<int> surroundings = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        minefield = FindObjectOfType<Camera>().GetComponent<Minefield>();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    private void OnMouseOver()
    {
        {
            if (Input.GetMouseButtonUp(0))
            {
                minefield.LeftClick(index);
            }
            if (Input.GetMouseButtonUp(1))
            {
                minefield.RightClick(index);
            }
            if (Input.GetMouseButtonUp(2))
            {
                //Debug.Log("Pressed middle click.");
            }
        }
    }

    public void UpdateSprite()
    {
        sprite.sprite = UpdatedSprite();
    }

    Sprite UpdatedSprite()
    {
        switch (state)
        {
            case State.Lock:
                return spriteLock;
            case State.Flag:
                return spriteFlag;
            case State.Explode:
                return spriteExplode;
            case State.Open:
                if (value == -1)
                {
                    return spriteMine;
                }
                else
                {
                    return spritesVal[value];
                }
            default:
                return spriteLock;
        }
    }


}