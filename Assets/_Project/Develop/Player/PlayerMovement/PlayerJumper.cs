using System;
using DG.Tweening;

/// <summary>
/// Отвечает за прыжки и всё с ними связанное
/// </summary>
public class PlayerJumper
{
    private PlayerData _playerData;
    private PlayerMover _playerMover;
    private bool _isJumping;
    private float _prevYPos;

    public PlayerJumper(PlayerMover playerMover, PlayerData playerData)
    {
        _playerMover = playerMover;
        _playerData = playerData;
    }

    public void OnJump()
    {
        if (!_playerMover.IsRunning)
            return;

        if (_isJumping)
            return;

        _playerMover.transform.DOMoveY(_playerMover.transform.position.y + _playerData.JumpHeight, _playerData.JumpTime);
    }

    /// <summary>
    /// Определяем, летит ли игрок или на земле
    /// </summary>
    public void CheckYPos()
    {
        var delta = _playerMover.transform.position.y - _prevYPos;
        if (Math.Abs(delta) > 0.01)
        {
            _isJumping = true;
        }
        else
        {
            _isJumping = false;
        }
    }

    public void OnFixedUpdate()
    {
        CheckYPos();
        _prevYPos = _playerMover.transform.position.y;
    }
}