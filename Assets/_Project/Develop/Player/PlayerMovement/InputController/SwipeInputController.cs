using UnityEngine;

public class SwipeInputController : InputController
{
    private Vector3 _startTouchPos;
    private Vector3 _endTouchPos;

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startTouchPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _endTouchPos = Input.GetTouch(0).position;

            var swipeDelta = _endTouchPos - _startTouchPos;
            swipeDelta = Vector3.Normalize(swipeDelta);
            DefineDirection(swipeDelta);
        }
    }

    /// <summary>
    /// Определяем в какую стороную был свайп и отправляем команду
    /// </summary>
    /// <param name="swipeDelta"></param>
    private void DefineDirection(Vector3 swipeDelta)
    {
        // Влево или вправо
        if(Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            if(swipeDelta.x < 0)
            {
                 MoveLeftDone();
            }
            else
            {
                 MoveRightDone();
            }
        }
        //Вверх или вниз
        else
        {
            if(swipeDelta.y > 0)
            {
                JumpDone();
            }
        }
    }
}