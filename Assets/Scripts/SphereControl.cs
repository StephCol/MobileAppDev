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
        drag_position = transform.position;
        
    }

    public void dragActivated(Ray our_ray, float destination)
    {
        
        Plane plane = new Plane(Vector3.up, Vector3.down);

       
        
        if (plane.Raycast(our_ray, out destination))
        {
            
            Vector3 pointalongplane = our_ray.origin + (our_ray.direction * destination);
            pointalongplane.y = -0.4f;
            print(pointalongplane);
            drag_position = pointalongplane;
            
        }

        
    }

    public void tapActivated()
    {
        
    }

    public void Update()
    {
        if (is_selected)
            transform.position = drag_position;
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
