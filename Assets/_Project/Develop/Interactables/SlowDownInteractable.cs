public class SlowDownInteractable : Interactable
{
    protected override void Interact()
    {
        _signalBus.Fire(new InteractablePickedSignal(InteractableType.SlowDownBonus));
    }
}