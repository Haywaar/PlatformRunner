using System;
using UnityEngine;
using Zenject;
using DG.Tweening;

/// <summary>
/// TODO - Когда впилим сюда нормального персонажа с паком анимаций - расширим класс
/// А пока пусть кубик катается и дёргается :)
/// </summary>
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject _animationMesh;
    [SerializeField] private ParticleSystem _deathParticleSystem;

    [Inject]
    private SignalBus _signalBus;

    private void Start()
    {
        _signalBus.Subscribe<PlayerDamagedSignal>(OnPlayerDamaged);
        _signalBus.Subscribe<PlayerDeadSignal>(OnPlayerDead);
    }

    private void OnPlayerDamaged()
    {
        _animationMesh.transform.DOShakePosition(0.7f, 0.75f, 15);
    }

    private void OnPlayerDead()
    {
        _animationMesh.gameObject.SetActive(false);
        _deathParticleSystem.Play();
    }
}