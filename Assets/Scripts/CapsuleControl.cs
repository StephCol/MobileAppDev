using UnityEngine;

public class CapsuleControl : MonoBehaviour, IInteractable
{
    Renderer my_renderer;
    bool is_selected = false;
    private Vector3 drag_position;
    Plane plane;

    public void dragActivated(Ray our_ray, float destination)
    {        
        plane = new Plane(((Camera.main.transform.position - transform.position).normalized), transform.position);

        if (plane.Raycast(our_ray, out destination))
            {
            Vector3 pointalongplane = our_ray.origin + (our_ray.direction * destination);
            pointalongplane.z = -4;
            drag_position = pointalongplane;
            }
    }

    public void reset()
    {
        transform.position = new Vector3(1.6f, 0.4f, -4f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.localScale = new Vector3(1, 1, 1);
        drag_position = transform.position;
        my_renderer.material.color = Color.white;
    }

    public void select_toggle(bool selected)
    {
        is_selected = selected;

        if (is_selected)
            my_renderer.material.color = Color.blue;
        else
            my_renderer.material.color = Color.white;
    }

    void Start()
    {
        my_renderer = GetComponent<Renderer>();
        drag_position = transform.position;
    }

    void Update()
    {
        if (is_selected)
            transform.position = drag_position;
    }

   

}
