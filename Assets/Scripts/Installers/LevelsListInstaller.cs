using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "LevelsListInstaller", menuName = "Data/LevelsListInstaller", order = 0)]
public class LevelsListInstaller : ScriptableObjectInstaller
{
    public LevelsList levelsList;
    public override void InstallBindings()
    {
        Container.BindInstance(levelsList);
    }
}
