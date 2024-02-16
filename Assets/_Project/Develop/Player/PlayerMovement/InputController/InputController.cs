using System;
using UnityEngine;

public abstract class InputController : MonoBehaviour
{
    public event Action MoveRight;
    public event Action MoveLeft;
    public event Action Jump;

    protected void MoveRightDone()
    {
        MoveRight?.Invoke();
    }

    protected void MoveLeftDone()
    {
        MoveLeft?.Invoke();
    }

    protected void JumpDone()
    {
        Jump?.Invoke();
    }
}