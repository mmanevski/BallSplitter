using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class LevelLoaded : UnityEvent<Level>
{
    
}

public class LevelManager : MonoBehaviour
{
    [Inject] private LevelsList levelsList;
    
    public Transform playAreaHolder;
    private Vector3 startRotation;
    private LevelDefine currentLevelDefine;
    
    public static LevelLoaded levelLoaded = new LevelLoaded();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.rewquestNewLevel.AddListener(OnRequestNewLevel);

    }

    private void OnRequestNewLevel()
    {
        currentLevelDefine = levelsList.allLevels[0];

        Level _newLevel = Instantiate(currentLevelDefine.level, playAreaHolder).GetComponent<Level>();
        _newLevel.Init();
        levelLoaded.Invoke(_newLevel);
    }
    
    private void OnDestroy()
    {

        GameManager.rewquestNewLevel.RemoveListener(OnRequestNewLevel);
    }
}
