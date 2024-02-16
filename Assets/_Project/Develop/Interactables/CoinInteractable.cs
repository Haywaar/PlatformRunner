using UnityEngine;
using DG.Tweening;

public class CoinInteractable : Interactable
{
    [SerializeField] private Transform _meshTransform;

    private void Awake()
    {
        var tween = _meshTransform.DOLocalRotate(new Vector3(0, 0, 360), 3.5f, RotateMode.FastBeyond360);
        tween.onComplete += () => _meshTransform.DORestart();
    }

    protected override void Interact()
    {
        _signalBus.Fire(new InteractablePickedSignal(InteractableType.Coin));
    }
}
