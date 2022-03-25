using UnityEngine;

public interface ITouchController
{
    //Object controls
    void tap(Vector2 position);
    void dragObject(Vector2 current_position);
    void scaleObject(float relativeDistance);
    void objectScaleEnded();
    void objectRotate(float angle);
    void objectRotateEnded();

    //Camera controls
    void cameraPan(Vector3 hit_position, Vector3 camera_position, Vector3 current_position);
    void cameraPanEnded();
    void cameraRotate(Vector3 first_touch_position, Vector3 position);
    void cameraRotateEnded();
    void cameraZoom(Touch first_touch, Touch second_touch);
    void cameraZoomEnded();
    void reset();
    void cameraReset();
}
