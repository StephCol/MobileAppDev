using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManagement : MonoBehaviour, ITouchController
{
    IInteractable selectedObject;

    //Objct drag
    float starting_distance_to_selected_object;
    Ray our_ray;

    //Object scale
    private Vector3 objectStartingScale;

    //Object rotation
    private Quaternion objectStartingOrientaton;

    //Camera Rotation
    private float xAngle = 0;
    private float yAngle = 0;
    private Vector3 firstpoint = new Vector3(0, 0, 0);
    private Vector3 secondpoint = new Vector3(0, 0, 0);
    float xAngTemp = 0;
    float yAngTemp = 0;

    //Object status
    private bool objectScaleStarted = false;
    private bool objectRotateStarted = false;

    //Camera status
    private bool cameraPanStarted;
    private bool cameraRotateStarted;
    private bool cameraZoomStarted;

    void Start()
    {
        Camera.main.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
    }

    public void dragObject(Vector2 current_position)
    {
        if (selectedObject != null)
        {
            our_ray = Camera.main.ScreenPointToRay(current_position);
            selectedObject.dragActivated(our_ray, starting_distance_to_selected_object);
        }
    }

    public void scaleObject(float relativeDistance) 
    {
        if (!objectScaleStarted)
        {
            if (selectedObject != null)
                objectStartingScale = ((MonoBehaviour)selectedObject).transform.localScale;
            else
                objectStartingScale = Camera.main.transform.localScale;

            objectScaleStarted = true;
        }

        if (selectedObject != null)
            ((MonoBehaviour)selectedObject).transform.localScale = objectStartingScale * relativeDistance;
        
    }

    public void objectScaleEnded()
    {
        objectScaleStarted = false;
    }

    public void cameraZoom(Touch touch1, Touch touch2)
    {
        float ZoomMinBound = 10f;
        float ZoomMaxBound = 150f;
        float TouchZoomSpeed = 0.1f;

        if (selectedObject == null)
        {

            if (cameraZoomStarted == false)
                cameraZoomStarted = true;

            Vector2 touch1Previous = touch1.position - touch1.deltaPosition;
            Vector2 touch2Previous = touch2.position - touch2.deltaPosition;

            float startingDistance = Vector2.Distance(touch1Previous, touch2Previous);
            float currentDistance = Vector2.Distance(touch1.position, touch2.position);

            float relativeDistance = startingDistance - currentDistance;
            if(startingDistance > 400)
            {
                Camera.main.fieldOfView += relativeDistance * TouchZoomSpeed;
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, ZoomMinBound, ZoomMaxBound);
            }
        }
    }

    public void cameraZoomEnded()
    {
        cameraZoomStarted = false;
    }

    public void cameraPan(Vector3 hit_position, Vector3 camera_position, Vector3 current_position)
    {
        if (selectedObject == null)
        {
            if(cameraPanStarted == false)
                cameraPanStarted = true;

            current_position.z = hit_position.z = camera_position.y;
            Vector3 direction = Camera.main.ScreenToWorldPoint(current_position) - Camera.main.ScreenToWorldPoint(hit_position);
            direction = direction * -1;
            Vector3 position = camera_position + (direction*3);
            Camera.main.transform.position = Vector3.Lerp(position, camera_position, Time.deltaTime);
        }
    }

    public void cameraPanEnded()
    {
        cameraPanStarted = false;
    }

    public void cameraRotate(Vector3 first_touch, Vector3 second_touch)
    {
        if (selectedObject == null)
        {
            if (cameraRotateStarted == false)
            {
                cameraRotateStarted = true;
                firstpoint = first_touch;
                xAngTemp = xAngle;
                yAngTemp = yAngle;
            }
            secondpoint = second_touch;

            xAngle = (float)(xAngTemp + (secondpoint.x - firstpoint.x) * 180.0 / Screen.width);
            yAngle = (float)(yAngTemp - (secondpoint.y - firstpoint.y) * 90.0 / Screen.height);

            Camera.main.transform.rotation = Quaternion.Euler(yAngle*-1, xAngle*-1, 0.0f);
        }
    }

    public void cameraRotateEnded()
    {
        cameraRotateStarted = false;
    }


    public void objectRotate(float angle)
    {

        if (!objectRotateStarted)
        {
            objectRotateStarted = true;
            if (selectedObject != null)
                objectStartingOrientaton = ((MonoBehaviour)selectedObject).transform.rotation;
        }
        else
        {
            angle = angle * Mathf.Rad2Deg;
            if (selectedObject != null)
                ((MonoBehaviour)selectedObject).transform.rotation = objectStartingOrientaton * Quaternion.AngleAxis(angle, Camera.main.transform.forward);
        }
    }

    public void objectRotateEnded()
    {
        objectRotateStarted = false;
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
                if (newObject != selectedObject)
                {
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
            else
            {
                if(selectedObject != null)
                {
                    selectedObject.select_toggle(false);
                    selectedObject = null;
                }
                
            }

        }
    }
}
