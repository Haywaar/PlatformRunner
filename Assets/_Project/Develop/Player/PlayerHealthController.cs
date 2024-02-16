using System.Collections;
using UnityEngine;
using Zenject;

/// <summary>
/// Обрабатывает коллизии, уменьшает здоровье в случае столкновения, отправляет сигнал о смерти в случае таковой
/// </summary>
public class PlayerHealthController : MonoBehaviour
{
    private PlayerData _playerData;
    private SignalBus _signalBus;
    private int _health;

    private bool _isImmortal;

    [Inject]
    private void Construct(PlayerData playerData, SignalBus signalBus)
    {
        _playerData = playerData;
        _health = _playerData.StartHealth;
        _signalBus = signalBus;

        _signalBus.Subscribe<PlayerDamagedSignal>(OnCollisionWithObstacle);
    }

    private void OnCollisionWithObstacle()
    {
        if (_isImmortal)
            return;

        StartCoroutine(MakeImmortalCoroutine());
        SubstractHealth();
        if (_health <= 0)
        {
            OnPlayerDead();
        }
    }

    private void SubstractHealth()
    {
        _health -= 1;
        _health = Mathf.Max(0, _health);
    }

    private void OnPlayerDead()
    {
        _signalBus.Fire(new PlayerDeadSignal());
    }

    /// <summary>
    /// Временно делаем игрока неуязвимым, чтобы после столкновения он случайно не бахнулся ещё раз
    /// </summary>
    private IEnumerator MakeImmortalCoroutine()
    {
        _isImmortal = true;
        yield return new WaitForSeconds(_playerData.InvinciblePeriodAfterDamage);
        _isImmortal = false;
    }
}