using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManagement : MonoBehaviour, ITouchController
{
    IInteractable selectedObject;
    float starting_distance_to_selected_object;
    Ray our_ray;


    public void drag(Vector2 current_position)
    {
        if (selectedObject != null)
        {
            our_ray = Camera.main.ScreenPointToRay(current_position);
            selectedObject.dragActivated(our_ray, starting_distance_to_selected_object);
        }
    }


    public void pinch(Vector2 position1, Vector2 position2, float relative_distance)
    {
        throw new System.NotImplementedException();
    }

    public void tap(Vector2 position)
    {

        our_ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit_info;
        
        if (Physics.Raycast(our_ray, out hit_info))
        {
            starting_distance_to_selected_object = Vector3.Distance(Camera.main.transform.position, hit_info.transform.position);
            IInteractable newObject = hit_info.transform.GetComponent<IInteractable>();

            if (newObject != null)
            {
                if (newObject != selectedObject) {
                    if (selectedObject != null)
                        selectedObject.select_toggle(false);

                    selectedObject = newObject;
                    selectedObject.select_toggle(true);
                }
                else
                {
                    newObject.select_toggle(false);
                    selectedObject = null;
                }
                    
            }
                
        }
        else
        {
            selectedObject.select_toggle(false);
            selectedObject = null;
        }
          
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
