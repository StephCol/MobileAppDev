using UnityEngine;

public class CubeControl : MonoBehaviour, IInteractable
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
        drag_position = our_ray.GetPoint(destination);
    }


    public void Update()
    {
        if(is_selected)
        transform.position = Vector3.Lerp(transform.position, drag_position, 0.5f);
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
        transform.position = new Vector3(0.04f, 0.1f, -5f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.localScale = new Vector3(1, 1, 1);
        drag_position = transform.position;
        my_renderer.material.color = Color.white;
    }

}
