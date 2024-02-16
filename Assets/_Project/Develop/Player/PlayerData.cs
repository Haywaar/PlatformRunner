using UnityEngine;

[System.Serializable]
public struct PlayerData
{
    [SerializeField] private int _startHealth;
    /// <summary>
    /// Время которое игрок неуязвим после получения урона
    /// </summary>
    [SerializeField] private float _invinciblePeriodAfterDamage;
    /// <summary>
    /// На сколько секунд мы останавливаем игрока после получения урона
    /// </summary>
    [SerializeField] private float _staggerDuration;
    /// <summary>
    /// Время за которое игрок переходит с одной дорожки на другую
    /// </summary>
    [SerializeField] private float _swapLaneTime;
    [Header("Jump params")]
    [SerializeField] private float _jumpHeight;
    /// <summary>
    /// Время за которое игрок долетает до пиковой точки
    /// </summary>
    [SerializeField] private float _jumpTime;
    [Header("Fly params")]
    [SerializeField] private float _flyHeight;
    [SerializeField] private float _flyDuration;


    public float SwapLaneTime { get => _swapLaneTime; }
    public float JumpHeight { get => _jumpHeight; }
    public float JumpTime { get => _jumpTime; }
    public float FlyHeight { get => _flyHeight; }
    public float FlyDuration { get => _flyDuration; }
    public int StartHealth { get => _startHealth; }
    public float InvinciblePeriodAfterDamage { get => _invinciblePeriodAfterDamage; }
    public float StaggerDuration { get => _staggerDuration; }
}