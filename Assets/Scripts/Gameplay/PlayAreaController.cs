using System;
using System.Collections;
using GameData;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class AnnounceAngle : UnityEvent<Vector3>
{
    
}

public class PlayAreaController : SingletonBehavior<PlayAreaController>
{
    [Inject] private GameParameters gameParameters; 
    
    public Transform playAreaHolder;
    private Vector3 startRotation;
    private float startRotY;

    public Level currentLevel;

    private bool isFlipped = false;
    
    public static AnnounceAngle announceAngle = new AnnounceAngle();
    public static UnityEvent playAreaFlipped = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        InputController.inputStartEvent.AddListener(HandleTouchStarted);
        InputController.inputChangeEvent.AddListener(HandleTouchMoved);
        InputController.inputEndEvent.AddListener(HandleTouchEnded);
        LevelManager.levelLoaded.AddListener(OnLevelLoaded);
        
    }

    private void OnLevelLoaded(Level level)
    {
        currentLevel = level;
    }

    private void HandleTouchEnded(Vector3 arg0)
    {
        //throw new System.NotImplementedException();
    }

    private void HandleTouchMoved(Vector3 moveChangeVec)
    {
        if (moveChangeVec.y > gameParameters.flipAreaTreshold && currentLevel.isFlippable)
        {
            StartCoroutine(FlipArea());
            return;
        }
        
        
        float _moveChange = Mathf.Clamp(moveChangeVec.x, -gameParameters.moveChangeRange, gameParameters.moveChangeRange);

        float _rotY = -startRotY + _moveChange *gameParameters.rotationFactor;
        _rotY = Utils.ClampAngle(-_rotY, -gameParameters.playAreaRotationRange, gameParameters.playAreaRotationRange);
        Vector3 _newRotation = new Vector3(startRotation.x, _rotY, startRotation.z);

        announceAngle.Invoke(_newRotation);
        playAreaHolder.eulerAngles = _newRotation;

    }

    IEnumerator FlipArea()
    {
        GameManager.gameState = GameState.Loading;
        float flipTime = 0.3f;
        float flipStep = 0.01f;
        float elapsedTime = 0f;

        float flipAngle = isFlipped ? 0f : 180f;
        
        Vector3 _newRotation = new Vector3(0f, 0f, flipAngle);

        while (elapsedTime < flipTime)
        {
            yield return new WaitForSeconds(flipStep);
            elapsedTime = elapsedTime + flipStep;
            playAreaHolder.eulerAngles = Vector3.Slerp(startRotation, _newRotation, elapsedTime/flipTime);
        }
        isFlipped = !isFlipped;
        playAreaFlipped.Invoke();
        GameManager.gameState = GameState.Playing;

        yield return null;
    }

    private void HandleTouchStarted(Vector3 arg0)
    {
        startRotation = playAreaHolder.rotation.eulerAngles;
        startRotY = Utils.ClampAngle(startRotation.y, -gameParameters.playAreaRotationRange,
            gameParameters.playAreaRotationRange);
    }
    
    void OnDestroy()
    {
        InputController.inputStartEvent.RemoveListener(HandleTouchStarted);
        InputController.inputChangeEvent.RemoveListener(HandleTouchMoved);
        InputController.inputEndEvent.RemoveListener(HandleTouchEnded);
        LevelManager.levelLoaded.RemoveListener(OnLevelLoaded);
        
    }

}
