using Zenject;

public class PlayerCoinController
{
    private SignalBus _signalBus;

    private int _coinsCount;

    public int CoinsCount { get => _coinsCount; }

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;

        _signalBus.Subscribe<InteractablePickedSignal>(OnInteractablePicked);
    }

    /// <summary>
    /// Проверяем, а вдруг мы подобрали именно монетку
    /// </summary>
    /// <param name="signal"></param>
    private void OnInteractablePicked(InteractablePickedSignal signal)
    {
        if (signal.InteractableType != InteractableType.Coin)
        {
            return;
        }

        AddCoins();
    }

    private void AddCoins()
    {
        _coinsCount++;
        _signalBus.Fire(new CoinsChangedSignal(_coinsCount));
    }
}