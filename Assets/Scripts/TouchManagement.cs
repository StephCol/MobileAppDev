using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManagement : MonoBehaviour
{
    IControllable selectedObject;
    float starting_distance_to_selected_object;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Ray ourRay = Camera.main.ScreenPointToRay(Input.touches[0].position);
   
            TouchPhase touchType = Input.touches[0].phase;
            print(touchType);
            

            Debug.DrawRay(ourRay.origin, 30 * ourRay.direction);

            RaycastHit info;
            if (Physics.Raycast(ourRay, out info))
            {
                IControllable objectHit = info.transform.GetComponent<IControllable>();
                if (objectHit != null && touchType == TouchPhase.Began)
                {
                    objectHit.youveBeenTapped();
                    selectedObject = objectHit;
                    starting_distance_to_selected_object = Vector3.Distance(Camera.main.transform.position, info.transform.position);
                }
            }

            //Assume selected object is not null, drag code goes here
            switch(Input.touches[0].phase)
            {
                case TouchPhase.Began:

                    break;
                case TouchPhase.Moved:
                    Ray new_positional_ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                    selectedObject.MoveTo(new_positional_ray.GetPoint(starting_distance_to_selected_object));
                    break;
                case TouchPhase.Stationary:

                    break;
                case TouchPhase.Ended:

                    break;
            }
        }

    }
}
