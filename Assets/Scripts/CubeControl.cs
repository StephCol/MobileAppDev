using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour, IInteractable
{
    Renderer my_renderer;
    bool is_selected = false;
    private Vector3 drag_position;
    MeshRenderer plane_renderer;

    //submission of project in the form of 5 or 6 projects
    //test scene or project so files can be pulled onto the project
    //create objects manually in a script - sphere, cube - & add component

    void Start()
    {
        my_renderer = GetComponent<Renderer>();
        drag_position = transform.position;
        Transform plane = transform.Find("Plane");
        plane.GetComponent<Collider>().enabled = false;
        plane_renderer = plane.GetComponent<MeshRenderer>();
        plane_renderer.forceRenderingOff = true;

    }

    public void dragActivated(Ray our_ray, float destination)
    {
        drag_position = our_ray.GetPoint(destination);
    }

    public void Update()
    {
        if(is_selected)
        transform.position = Vector3.Lerp(transform.position, drag_position, 0.5f);
    }

    public void tapActivated()
    {
        
    }


    public void select_toggle(bool selected)
    {
        is_selected = selected;

        if (is_selected)
            plane_renderer.forceRenderingOff = false;
        else
            plane_renderer.forceRenderingOff = true;

    }

    internal void do_cube_stuff()
    {
        print("Im a cube and Im OK");
    }
}
