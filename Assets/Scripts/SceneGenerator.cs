using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class SceneGenerator : MonoBehaviour
{
    GameObject sphere;
    GameObject cube;
    GameObject capsule;
    TouchManagement manager;
    IInteractable[] objects;


    void Start()
    {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);

        GameObject utilities = new GameObject("TouchManager");
        utilities.AddComponent<GestureIdentifier>();
        utilities.AddComponent<TouchManagement>();

        sphere.AddComponent<SphereControl>();
        sphere.transform.position = new Vector3(-1.25f, -0.4f, -4f);
        
        cube.AddComponent<CubeControl>();
        cube.transform.position = new Vector3(0.04f, 0.1f, -5f);
        
        capsule.AddComponent<CapsuleControl>();
        capsule.transform.position = new Vector3(1.6f, 0.4f, -4f);

        GameObject arenaPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        arenaPlane.transform.position = new Vector3(0.29f, 29.58f, 19.16f);
        arenaPlane.transform.rotation = Quaternion.Euler(90f, 180f, 0f);
        arenaPlane.transform.localScale = new Vector3(15f, 16.4f, 6.09f);
        arenaPlane.GetComponent<Renderer>().material.color = Color.black;

        GameObject groundPLane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        groundPLane.transform.position = new Vector3(0.31f, -0.99f, -54.7f);
        groundPLane.transform.localScale = new Vector3(15f, 14.7f, 15f);
        groundPLane.GetComponent<Renderer>().material.color = new Color32(40, 118, 40, 1);

        Camera camera = Camera.main;        
        camera.transform.position = new Vector3(0f, 0.52f, -15.54f);
        camera.transform.localScale = new Vector3(1f, 1f, 1f);
        camera.fieldOfView = 60f;

        Canvas c;
        GameObject canvasObject = new GameObject();
        canvasObject.name = "CanvasOb";
        canvasObject.AddComponent<Canvas>();

        c = canvasObject.GetComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();
        CanvasScaler cScaler = canvasObject.GetComponent<CanvasScaler>();
        cScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasObject.AddComponent<GraphicRaycaster>();

        GameObject resetButton = DefaultControls.CreateButton(new DefaultControls.Resources());
        resetButton.transform.SetParent(c.transform, false);
        resetButton.GetComponentInChildren<Text>().text = "Reset";
        resetButton.GetComponentInChildren<RectTransform>().localScale = new Vector2(3f, 3f);
        resetButton.GetComponentInChildren<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 50f, 20f);
        resetButton.GetComponentInChildren<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 100f, 50f);

        resetButton.GetComponent<Button>().onClick.AddListener(resetGame);


        manager = FindObjectOfType<TouchManagement>();
        objects = FindObjectsOfType<MonoBehaviour>().OfType<IInteractable>().ToArray();

    }

    private void resetGame()
    {
        manager.reset();
        foreach(IInteractable o in objects)
        {
            o.reset();
        }

        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);

        Camera camera = Camera.main;
        camera.transform.position = new Vector3(0f, 0.52f, -15.54f);
        camera.transform.localScale = new Vector3(1f, 1f, 1f);
        camera.fieldOfView = 60f;
    }

}
