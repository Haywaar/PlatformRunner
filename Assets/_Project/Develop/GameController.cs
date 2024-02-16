using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameController : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;

    private SignalBus _signalBus;
    private PlayerCoinController _playerCoinController;

    [Inject]
    private void Construct(SignalBus signalBus, PlayerCoinController playerCoinController)
    {
        _signalBus = signalBus;
        _playerCoinController = playerCoinController;
    }

    private void Start()
    {
        _startGameButton.onClick.AddListener(() =>
        {
            StartGame();
            _startGameButton.gameObject.SetActive(false);
        });

        _signalBus.Subscribe<PlayerDeadSignal>(OnPlayerDead);
    }

    private void OnPlayerDead()
    {
        StopGame();
        var youLoseDialog = DialogManager.ShowDialog<YouLoseDialog>();
        youLoseDialog.Init(_playerCoinController.CoinsCount);
    }

    private void StartGame()
    {
        _signalBus.Fire<GameStartedSignal>();
    }

    private void StopGame()
    {
        _signalBus.Fire<GameStopSignal>();
    }
}