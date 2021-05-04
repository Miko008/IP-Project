using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Renderer rend;
    public Color color_ori,
                 color_hover = Color.white,
                 color_disabled = Color.black;
    private bool clicked = false,
                 can_place = true,
                 can_bomb = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        color_ori = rend.material.color;
    }
    void OnMouseEnter()
    {
        if(!clicked)
            rend.material.color = color_hover;
    }
    void OnMouseExit()
    {
        if(!clicked)
            rend.material.color = color_ori;
    }
    public void Click()
    {
        clicked = true;
        can_bomb = false;
        //this.GetComponent<Collider>().enabled = false;
        rend.material.color = color_disabled;
    }
    public void DisablePlacing()
    {
        can_place = false;
    }
    public bool CanPlace()
    {
        return can_place;
    }
    public void EnableBombing()
    {
        can_bomb = true;
    }
    public bool CanBomb()
    {
        return can_bomb;
    }
}

