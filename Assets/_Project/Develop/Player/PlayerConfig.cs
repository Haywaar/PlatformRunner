using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/PlayerConfig", order = 1)]
public class PlayerConfig : ScriptableObject
{
    [SerializeField] private PlayerData _playerData;

    public PlayerData PlayerData { get => _playerData; }
} 
