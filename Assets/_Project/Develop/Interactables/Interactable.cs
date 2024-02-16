using UnityEngine;
using Zenject;

public abstract class Interactable : MonoBehaviour
{
    protected SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            Interact();
            Dispose();
        }
    }

    protected abstract void Interact();

    private void Dispose()
    {
        gameObject.SetActive(false);
    }
}
