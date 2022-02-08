using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour, IInteractable
{
    Renderer my_renderer;
    bool is_selected = false;
    private Vector3 drag_position;

    //submission of project in the form of 5 or 6 projects
    //test scene or project so files can be pulled onto the project
    //create objects manually in a script - sphere, cube - & add component

    void Start()
    {
        my_renderer = GetComponent<Renderer>();
    }

    public void dragActivated(Vector3 destination)
    {
        drag_position = destination;
        //transform.position = Vector3.Lerp(transform.position, drag_position, 0.9f);
    }

    public void Update()
    {
        if(is_selected)
            transform.position = Vector3.Lerp(transform.position, drag_position, 0.9f); ;
    }

    public void tapActivated()
    {
        
    }


    public void select_toggle(bool selected)
    {
        is_selected = selected;

        if (is_selected)
            my_renderer.material.color = Color.red;
        else
            my_renderer.material.color = Color.white;

    }

    internal void do_cube_stuff()
    {
        print("Im a cube and Im OK");
    }
}
