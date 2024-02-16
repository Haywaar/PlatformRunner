using UnityEngine;

public class KeyboardInputController : InputController
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRightDone();
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeftDone();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            JumpDone();
            return;
        }

    }
}