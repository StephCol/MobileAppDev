using System.Linq;
using UnityEngine;

public class GestureIdentifier : MonoBehaviour
{

    private float tap_timer;
    private bool has_moved;
    private float MAX_ALLOWED_TAP_TIME = 0.5f;
    private float startingAngle;
    ITouchController[] managers;
    private float startingPinchDistance;

    void Start()
    {
        managers = FindObjectsOfType<MonoBehaviour>().OfType<ITouchController>().ToArray();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            tap_timer += Time.deltaTime;
            Touch[] all_touches = Input.touches;
            Touch first_touch = all_touches[0];
            

            if (all_touches.Length > 1)
            {
                Touch second_touch = all_touches[1];
                
                if ((first_touch.phase == TouchPhase.Began) || (second_touch.phase == TouchPhase.Began))
                {
                    startingPinchDistance = Vector3.Distance(first_touch.position, second_touch.position);
                    startingAngle = Mathf.Atan2(second_touch.position.y - first_touch.position.y, second_touch.position.x - first_touch.position.x);
                }

                if ((first_touch.phase == TouchPhase.Moved) || (second_touch.phase == TouchPhase.Moved))
                {
                    float currentDistance = Vector3.Distance(first_touch.position, second_touch.position);
                    float currentAngle = Mathf.Atan2(second_touch.position.y - first_touch.position.y, second_touch.position.x - first_touch.position.x);

                    foreach (ITouchController manager in managers)
                        (manager as ITouchController).pinch(currentDistance / startingPinchDistance);

                    foreach (ITouchController manager in managers)
                        (manager as ITouchController).rotate(currentAngle - startingAngle);
                }

                if ((first_touch.phase == TouchPhase.Ended ) || (second_touch.phase == TouchPhase.Ended))
                {
                    foreach (ITouchController manager in managers)
                        (manager as ITouchController).pinchEnded();

                    foreach (ITouchController manager in managers)
                        (manager as ITouchController).rotateEnded();
                }

            }
            else
            {
                switch (first_touch.phase)
                {
                    case TouchPhase.Began:
                        tap_timer = 0f;
                        has_moved = false;

                        break;
                    case TouchPhase.Moved:
                        has_moved = true;

                        if ((tap_timer > MAX_ALLOWED_TAP_TIME) && has_moved)
                        {
                            foreach (ITouchController manager in managers)
                                (manager as ITouchController).drag(first_touch.position);
                        }
                        break;

                    case TouchPhase.Ended:
                        if ((tap_timer < MAX_ALLOWED_TAP_TIME) && !has_moved)
                        {
                            foreach (ITouchController manager in managers)
                                (manager as ITouchController).tap(first_touch.position);
                        }
                        break;

                }
            }

        }

    }
}
