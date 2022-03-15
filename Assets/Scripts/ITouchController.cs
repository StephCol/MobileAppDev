using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchController
{
    void tap(Vector2 position);
    void drag(Vector2 current_position);
    void pinch(float relativeDistance);
    void pinchEnded();
    void rotate(float angle);
    void rotateEnded();
}
