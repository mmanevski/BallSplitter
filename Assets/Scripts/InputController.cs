using UnityEngine;
using UnityEngine.Events;

public class InputChangeEvent : UnityEvent<Vector3>
{
}public class InputStartEvent : UnityEvent<Vector3>
{
}
public class InputEndEvent : UnityEvent<Vector3>
{
}

public class InputController : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;
   
    public static InputChangeEvent inputChangeEvent = new InputChangeEvent();
    public static InputStartEvent inputStartEvent = new InputStartEvent();
    public static InputEndEvent inputEndEvent = new InputEndEvent();

    public Vector2 vec;

    public Vector2 MoveChange;
    private Vector2 prevPoint;


    private void Awake()
    {
        InputListener.TouchScreen += InputListener_TouchScreen;
        InputListener.ReleaseScreen += InputListener_ReleaseScreen;
      
    }

    void InputListener_ReleaseScreen(Vector3 obj)
    {
        inputEndEvent.Invoke(startPos);
    }

    void InputListener_TouchScreen(Vector3 obj)
    {
        startPos = InputListener.InputViewportPosition;
        inputStartEvent.Invoke(startPos);
    }
    
    
    private void FixedUpdate()
    {
        float MaxDist = 2000f;

        if (InputListener.mouseDown)
        {
            endPos = InputListener.InputViewportPosition;

            MoveChange = endPos - startPos;
            
            //prevPoint = endPos;
            
            inputChangeEvent.Invoke(MoveChange);
        }

    }

    private void OnDestroy()
    {
        InputListener.TouchScreen -= InputListener_TouchScreen;
    }
}
