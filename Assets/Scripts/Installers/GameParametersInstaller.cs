using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameParametersInstaller", menuName = "Data/GameParametersInstaller", order = 0)]
public class GameParametersInstaller : ScriptableObjectInstaller
{
    public GameParameters gameParameters;
    public override void InstallBindings()
    {
        Container.BindInstance(gameParameters);
    }
}