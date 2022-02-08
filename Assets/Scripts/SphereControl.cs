using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControl : MonoBehaviour, IInteractable
{
    Renderer my_renderer;
    bool is_selected = false;
    private Vector3 drag_position;

    void Start()
    {
        my_renderer = GetComponent<Renderer>();
    }

    public void dragActivated(Vector3 destination)
    {
        drag_position = destination;
    }

    public void tapActivated()
    {
        
    }

    public void Update()
    {
        //if (is_selected)
         //   transform.position = drag_position;
    }

    public void select_toggle(bool selected)
    {
        is_selected = selected;

        if (is_selected)
            my_renderer.material.color = Color.red;
        else
            my_renderer.material.color = Color.white;

    }
}
