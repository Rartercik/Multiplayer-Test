using UnityEngine;
using Zenject;

public class PlayerStartInformationInstaller : MonoInstaller
{
    [SerializeField] GameEndWindow _gameEndWindow;

    public override void InstallBindings()
    {
        Container.Bind<GameEndWindow>().FromInstance(_gameEndWindow).AsSingle().NonLazy();
    }
}