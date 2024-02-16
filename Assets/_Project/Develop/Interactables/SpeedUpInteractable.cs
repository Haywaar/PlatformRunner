public class SpeedUpInteractable : Interactable
{
    protected override void Interact()
    {
        _signalBus.Fire(new InteractablePickedSignal(InteractableType.SpeedUpBonus));
    }
}