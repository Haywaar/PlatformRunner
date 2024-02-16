using DG.Tweening;
using UnityEngine;

/// <summary>
/// Находится внутри PlayerMover и отвечает за движения влево вправо, чтобы не засорять PlayerMover
/// </summary>
public class PlayerSideMover
{
    private PlayerMover _playerMover;
    private PlayerData _playerData;
    private ChunkData _chunkData;

    /// <summary>
    ///  Персонаж в процессе перехода на другую колею
    /// </summary>
    private bool _switchingLanes;

    /// <summary>
    /// 0 - левая, 1 центральная, 2 - правая
    /// </summary>
    private int _laneId = 1;

    public PlayerSideMover(PlayerMover playerMover, PlayerData playerData, ChunkData chunkData)
    {
        _playerMover = playerMover;
        _playerData = playerData;
        _chunkData = chunkData;
    }

    /// <summary>
    /// Сдвиг персонажа влево/вправо
    /// </summary>
    /// <param name="movementSide"></param>
    public void OnMoveSide(MovementSide movementSide)
    {
        if (!_playerMover.IsRunning)
            return;

        int laneDelta;
        if (movementSide == MovementSide.Left)
        {
            laneDelta = -1;
        }
        else
        {
            laneDelta = 1;
        }

        // Уже на крайней дорожке и двигаться дальше нельзя
        if (_laneId + laneDelta < 0 || _laneId + laneDelta > 2)
        {
            return;
        }

        _laneId += laneDelta;

        //В процессе перехода между дорожками
        if (_switchingLanes)
        {
            return;
        }

        if (_playerMover.transform.position.x == laneDelta)
        {
            return;
        }

        _switchingLanes = true;
        var tween = _playerMover.transform.DOMoveX(GetXLanePos(), _playerData.SwapLaneTime);
        tween.onComplete += () =>
        {
            _switchingLanes = false;
        };
    }

    private float GetXLanePos()
    {
        switch (_laneId)
        {
            case 0:
                return _chunkData.LeftLaneXPos;
            case 1:
                return _chunkData.MiddleLaneXPos;
            case 2:
                return _chunkData.RightLaneXPos;

            default:
                Debug.LogError("Something wrong with GetLanePos! _laneId " + _laneId);
                return 1;
        }
    }
}