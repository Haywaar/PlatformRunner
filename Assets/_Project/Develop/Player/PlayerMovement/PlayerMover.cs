using UnityEngine;
using Zenject;
using DG.Tweening;
using System.Collections;

/// <summary>
/// Принимает команды от InputController-а на перемещение влево/вправо/прыжки
/// А также за полёты от бонусов
/// </summary>
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private bool _isRunning;
    private SignalBus _signalBus;
    private PlayerData _playerData;
    private InputController _inputController;
    private LevelController _levelController;
    private ChunkData _chunkData;

    private PlayerSideMover _playerSideMover;
    private PlayerJumper _playerJumper;
    private int _chunkPassedQuantity;
    public bool IsRunning { get => _isRunning; }

    [Inject]
    private void Construct(SignalBus signalBus, LevelController levelController, PlayerData playerData, InputController inputController, ChunkData chunkData)
    {
        _signalBus = signalBus;
        _playerData = playerData;
        _inputController = inputController;
        _levelController = levelController;

        _chunkData = chunkData;
        _playerSideMover = new PlayerSideMover(this, playerData, chunkData);
        _playerJumper = new PlayerJumper(this, playerData);
    }

    private void Start()
    {
        _signalBus.Subscribe<GameStartedSignal>(() => { _isRunning = true; });
        _signalBus.Subscribe<GameStopSignal>(() => { _isRunning = false; });

        _signalBus.Subscribe<InteractablePickedSignal>(OnInteractablePicked);

        _inputController.MoveRight += () => _playerSideMover.OnMoveSide(MovementSide.Right);
        _inputController.MoveLeft += () => _playerSideMover.OnMoveSide(MovementSide.Left);
        _inputController.Jump += _playerJumper.OnJump;
    }

    private void OnInteractablePicked(InteractablePickedSignal signal)
    {
        if (signal.InteractableType != InteractableType.Wings)
        {
            return;
        }

        StartCoroutine(FlyCoroutine());
    }

    private IEnumerator FlyCoroutine()
    {
        transform.DOMoveY(transform.position.y + _playerData.FlyHeight, _playerData.JumpTime);
        _rigidbody.useGravity = false;
        yield return new WaitForSeconds(_playerData.FlyDuration);
        _rigidbody.useGravity = true;
    }

    private void FixedUpdate()
    {
        if (_isRunning)
        {
            transform.Translate(new Vector3(0, 0, 1) * _levelController.CurrentSpeed * (0.1f));

            // от Z вычитаем 7 чтобы чанк исчезал когда он скрывался за камерой а не сразу
            int chunkPassedQuantity = ((int)transform.position.z - 7) / (int)_chunkData.ChunkLength;
            if (chunkPassedQuantity > _chunkPassedQuantity)
            {
                _chunkPassedQuantity = chunkPassedQuantity;
                _signalBus.Fire(new ChunkPassedSignal());
            }

            _playerJumper.OnFixedUpdate();
        }

        CheckIfOutOfBounds();
    }

    /// <summary>
    /// У меня не хватило времени отточить физику так чтобы в edge-case-ах игрок не падал под текстуру посему 
    /// Пусть телепортирует наверх
    /// </summary>
    private void CheckIfOutOfBounds()
    {
        if (transform.position.y < -5.0f)
        {
            transform.position += new Vector3(0, 20f, 0);
        }
    }
}
