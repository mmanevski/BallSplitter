using UnityEngine;
using System;

public class InputListener: MonoBehaviour
{
    public static event Action<Vector3> TouchScreen = v3 => { };
    public static event Action<Vector3> ReleaseScreen = v3 => { };
    public static event Action<Vector3> HoldScreen = v3 => { };
    public static bool mouseDown;
    public static Vector3 InputWorldPosition;
    public static Vector3 InputViewportPosition;
    public static Vector3 InputTouchPosition;
    public static Vector3 InputWorldPositionStart;
    public static Vector3 InputViewportPositionStart;
    public static Camera camera;

    public Vector3 TopViewportCutoff;

    void Awake()
    {
        camera = Camera.main;
        if (camera == null)
            camera = FindObjectOfType<Camera>();
    }

    public void Reset()
    {
        mouseDown = false;
    }

    void Update()
    {
        UpdateMouseInput();
    }

    void UpdateMouseInput()
    {
        Vector3 touchPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 touchWorldPosition = camera.ScreenToWorldPoint(touchPosition);
        Vector3 touchViewportPosition = camera.ScreenToViewportPoint(touchPosition);

        InputTouchPosition = touchPosition;
        InputWorldPosition = touchWorldPosition;
        InputViewportPosition = touchViewportPosition;

        if (Input.GetMouseButtonDown(0))
        {
            InputWorldPositionStart = InputWorldPosition;
            InputViewportPositionStart = InputViewportPosition;

            TouchScreen(Input.mousePosition);
            mouseDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            InputViewportPositionStart = InputViewportPosition - InputViewportPositionStart;
            ReleaseScreen(Input.mousePosition);
            mouseDown = false;
        }
        else if (Input.GetMouseButton(0))
        {
            InputViewportPositionStart = InputViewportPosition - InputViewportPositionStart;

            HoldScreen(Input.mousePosition);
        }
    }
}