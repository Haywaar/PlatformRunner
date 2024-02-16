using System;
using TMPro;
using UnityEngine;
using Zenject;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsCount;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<CoinsChangedSignal>(OnCoinsChanged);
    }

    private void OnCoinsChanged(CoinsChangedSignal signal)
    {
        _coinsCount.text = string.Format("Coins:{0}", signal.Value);
    }
}
