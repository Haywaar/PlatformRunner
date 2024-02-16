using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Класс участка дороги на котором расположены препятствия и монетки
/// </summary>
public class Chunk : MonoBehaviour
{
    [SerializeField] private int _id;
    private List<Interactable> _interactables = new List<Interactable>();

    public int Id { get => _id; }

    private DiContainer _diContainer;

    [Inject]
    private void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer; 
    }

    private void Start()
    {
        // Понимаю, что GetComponentsInChildren - это зло, но ГДшники очень устанут ручками прокидывать
        // Все Interactabl-ы в чанк :/ 
        // Плюс из-за пула это делается один раз при создании игры
        foreach(var interactable in GetComponentsInChildren<Interactable>())
        {
            _diContainer.Inject(interactable);
            _interactables.Add(interactable);
        }
    }

    private void OnEnable()
    {
        ActivateInteractables(true);
    }

    private void ActivateInteractables(bool active)
    {
        foreach(var interactable in _interactables)
        {
            interactable.gameObject.SetActive(active);
        }
    }
}