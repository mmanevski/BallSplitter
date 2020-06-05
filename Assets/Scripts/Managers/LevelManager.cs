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
    private int currentLevel = -1;
    
    public Transform playAreaHolder;
    private Vector3 startRotation;
    private LevelDefine currentLevelDefine;
    
    public static LevelLoaded levelLoaded = new LevelLoaded();

    public GameObject currentLevelObject;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.rewquestNewLevel.AddListener(OnRequestNewLevel);

    }

    private void OnRequestNewLevel()
    {
        if (currentLevelObject != null)
            Destroy(currentLevelObject);
        
        currentLevel++;
        if (currentLevel >= levelsList.allLevels.Count)
            currentLevel = 0;
        currentLevelDefine = levelsList.allLevels[currentLevel];

        currentLevelObject = Instantiate(currentLevelDefine.level, playAreaHolder);
        Level _newLevel = currentLevelObject.GetComponent<Level>();
        _newLevel.Init();
        levelLoaded.Invoke(_newLevel);
    }
    
    private void OnDestroy()
    {

        GameManager.rewquestNewLevel.RemoveListener(OnRequestNewLevel);
    }
}
