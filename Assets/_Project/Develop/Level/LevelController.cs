using System.Collections;
using UnityEngine;
using Zenject;

/// <summary>
/// Класс который следит за изменением скорости уровня, с учетом течения времени и использования бонусов
/// </summary>
public class LevelController : MonoBehaviour
{
    private float _currentSpeed;
    private LevelData _levelData;
    private SignalBus _signalBus;
    private bool _isRunning;
    private float _timePassed;

    public float CurrentSpeed { get => _currentSpeed; }
    private float _speedKoef = 1.0f;
    private Coroutine _changeSpeedCoroutine;
    private PlayerData _playerData;

    [Inject]
    private void Construct(LevelData levelData, SignalBus signalBus, PlayerData playerData)
    {
        _levelData = levelData;
        _signalBus = signalBus;
        _playerData = playerData;
    }

    private void Start()
    {
        _signalBus.Subscribe<GameStartedSignal>(StartRunning);
        _signalBus.Subscribe<InteractablePickedSignal>(OnInteractablePicked);

        _signalBus.Subscribe<PlayerDamagedSignal>(() => ChangeSpeedKoef(-0.2f, _playerData.StaggerDuration));

        _signalBus.Subscribe<GameStopSignal>(StopRunning);
    }

    private void ChangeSpeedKoef(float speedKoef, float delayTime)
    {
        if (_changeSpeedCoroutine != null)
        {
            StopCoroutine(_changeSpeedCoroutine);
        }

        _changeSpeedCoroutine = StartCoroutine(ChangeSpeedKoefCoroutine(speedKoef, delayTime));
    }

    private IEnumerator ChangeSpeedKoefCoroutine(float speedKoef, float delayTime)
    {
        _speedKoef = speedKoef;
        yield return new WaitForSeconds(delayTime);
        _speedKoef = 1.0f;
    }

    private void StopRunning()
    {
        _isRunning = false;
        _speedKoef = 1.0f;
    }

    private void StartRunning()
    {
        _isRunning = true;
    }

    private void Update()
    {
        if (_isRunning)
        {
            _timePassed += Time.deltaTime;
            _currentSpeed = Mathf.Lerp(_levelData.MinSpeed, _levelData.MaxSpeed, (_timePassed / _levelData.TimeUntilMaxSpeed));

            _currentSpeed *= _speedKoef;
        }
    }

    private void OnInteractablePicked(InteractablePickedSignal signal)
    {
        switch (signal.InteractableType)
        {
            case InteractableType.Wings:
                ChangeSpeedKoef(_levelData.SpeedUpKoef, _playerData.FlyDuration);
                break;
            case InteractableType.SpeedUpBonus:
                ChangeSpeedKoef(_levelData.SpeedUpKoef, _levelData.SpeedUpKoefLength);
                break;
            case InteractableType.SlowDownBonus:
                ChangeSpeedKoef(_levelData.SlowDownKoef, _levelData.SlowDownKoefLength);
                break;
            default:
                //Ничего не делаем
                break;
        }
    }
}
