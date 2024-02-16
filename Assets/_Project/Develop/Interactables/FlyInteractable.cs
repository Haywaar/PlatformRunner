using UnityEngine;

public class FlyInteractable : Interactable
{
    protected override void Interact()
    {
        _signalBus.Fire(new InteractablePickedSignal(InteractableType.Wings));
    }
}