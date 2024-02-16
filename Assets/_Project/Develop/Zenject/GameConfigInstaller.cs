using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "Installers/GameConfigInstaller")]
public class GameConfigInstaller : ScriptableObjectInstaller<GameConfigInstaller>
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private ChunksConfig _chunkConfig;
    [SerializeField] private LevelConfig _levelConfig;


    public override void InstallBindings()
    {
        Container.Bind<PlayerData>().FromInstance(_playerConfig.PlayerData);
        Container.Bind<ChunkData>().FromInstance(_chunkConfig.ChunkData);
        Container.Bind<LevelData>().FromInstance(_levelConfig.LevelData);
    }
}