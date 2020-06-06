using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class LevelLoaded : UnityEvent<Level>
{
    
}

public class LevelManager : MonoBehaviour
{
    public bool overrideLevelList = true;
    public LevelDefine overrideDefine;
    
    public Transform playAreaHolder;
    public GameObject currentLevelObject;
    
    [Inject] private LevelsList levelsList;
    private int currentLevel = -1;
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
        if (currentLevelObject != null)
            Destroy(currentLevelObject);
        
        currentLevel++;
        if (currentLevel >= levelsList.allLevels.Count)
            currentLevel = 0;
        currentLevelDefine = levelsList.allLevels[currentLevel];

        if (overrideLevelList)
            currentLevelDefine = overrideDefine;

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
