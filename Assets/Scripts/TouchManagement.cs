using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManagement : MonoBehaviour, ITouchController
{
    IInteractable selectedObject;
    float starting_distance_to_selected_object;
    Ray our_ray;
    private Vector3 startingScale;
    private bool pinchStarted = false;
    public float rotatespeed = 50f;
    private bool rotateStarted;
    private Quaternion startingOrientaton;

    public void drag(Vector2 current_position)
    {
        if (selectedObject != null)
        {
            our_ray = Camera.main.ScreenPointToRay(current_position);
            selectedObject.dragActivated(our_ray, starting_distance_to_selected_object);
        }
    }

    public void pinch(float relativeDistance) 
    {
        if (!pinchStarted)
        {
            if (selectedObject != null)
                startingScale = ((MonoBehaviour)selectedObject).transform.localScale;
            else
                startingScale = Camera.main.transform.localScale;

            pinchStarted = true;
        }

        if (selectedObject != null)
            ((MonoBehaviour)selectedObject).transform.localScale = startingScale * relativeDistance;
        
    }

    public void cameraPinch(Touch touch1, Touch touch2)
    {
        float ZoomMinBound = 20f;
        float ZoomMaxBound = 110f;
        float TouchZoomSpeed = 0.1f;

        if (selectedObject == null)
        {
            Vector2 touch1Previous = touch1.position - touch1.deltaPosition;
            Vector2 touch2Previous = touch2.position - touch2.deltaPosition;

            float startingDistance = Vector2.Distance(touch1Previous, touch2Previous);
            float currentDistance = Vector2.Distance(touch1.position, touch2.position);

            float relativeDistance = startingDistance - currentDistance;

            Camera.main.fieldOfView += relativeDistance * TouchZoomSpeed;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, ZoomMinBound, ZoomMaxBound);
        }
    }

    public void pinchEnded()
    {
        pinchStarted = false;
    }

    public void rotate(float angle)
    {

        if (!rotateStarted)
        {
            rotateStarted = true;
            if (selectedObject != null)
                startingOrientaton = ((MonoBehaviour)selectedObject).transform.rotation;
        }
        else
        {
            angle = angle * Mathf.Rad2Deg;
            if (selectedObject != null)
                ((MonoBehaviour)selectedObject).transform.rotation = startingOrientaton * Quaternion.AngleAxis(angle, Camera.main.transform.forward);
        }
    }

    public void rotateEnded()
    {
        rotateStarted = false;
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
                selectedObject.select_toggle(false);
                selectedObject = null;
            }

        }
    }
}
