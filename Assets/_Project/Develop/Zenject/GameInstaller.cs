using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private LevelController _levelController;
    [SerializeField] private SwipeInputController _swipeInputController;
    [SerializeField] private KeyboardInputController _keyboardInputController;

    public override void InstallBindings()
    {
        BindSignals();
        Container.BindInstance(_levelController);

#if UNITY_ANDROID
        Container.Bind<InputController>().FromInstance(_swipeInputController);
#else
        Container.Bind<InputController>().FromInstance(_keyboardInputController);
#endif

        var playerCoinController = new PlayerCoinController();
        Container.BindInstance(playerCoinController);
        Container.QueueForInject(playerCoinController);
    }

    private void BindSignals()
    {
        SignalBusInstaller.Install(Container);
        
        Container.DeclareSignal<GameStartedSignal>().OptionalSubscriber();
        Container.DeclareSignal<GameStopSignal>().OptionalSubscriber();
        Container.DeclareSignal<ChunkPassedSignal>().OptionalSubscriber();

        Container.DeclareSignal<CoinsChangedSignal>().OptionalSubscriber();

        Container.DeclareSignal<InteractablePickedSignal>().OptionalSubscriber();

        Container.DeclareSignal<PlayerDeadSignal>().OptionalSubscriber();
        Container.DeclareSignal<PlayerDamagedSignal>().OptionalSubscriber();
    }
}