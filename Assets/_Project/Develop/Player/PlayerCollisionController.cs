using UnityEngine;
using Zenject;

/// <summary>
/// Следит за столкновениями с препятствиями, а также за тем,
/// Находится ли игрок в воздухе или на земле
/// </summary>
public class PlayerCollisionController : MonoBehaviour
{
    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            _signalBus.Fire(new PlayerDamagedSignal());
        }
    }
}