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
            drag_position = pointalongplane;
        }
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
            my_renderer.material.color = Color.blue;
        else
            my_renderer.material.color = Color.white;
    }

    public void reset()
    {
        transform.position = new Vector3(-1.25f, -0.4f, -4f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.localScale = new Vector3(1, 1, 1);
        drag_position = transform.position;
        my_renderer.material.color = Color.white;
    }
}
