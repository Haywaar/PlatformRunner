using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Занимается спавном и деспавном участков трасс с препятствием
/// </summary>
public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private Transform _chunkRoot;
    private SignalBus _signalBus;
    private int _prevChunkId = -1;

    private int _totalChunkCount = 0;

    private ChunkData _chunkData;
    private ChunkPool _pool;
    private Queue<Chunk> _queue = new Queue<Chunk>();

    [Inject]
    private void Construct(SignalBus signalBus, ChunkData chunkData, DiContainer diContainer)
    {
        _signalBus = signalBus;
        _chunkData = chunkData;
        _pool = new ChunkPool(chunkData, diContainer, _chunkRoot);
    }

    private void Start()
    {
        SpawnStartChunk();
        for (int i = 0; i < _chunkData.ChunkPrewarmCount; i++)
        {
            SpawnRandomChunk();
        }

        _signalBus.Subscribe<ChunkPassedSignal>(OnChunkPassed);
    }

    private void OnChunkPassed()
    {
        SpawnRandomChunk();
        DespawnChunk();
    }

    private void SpawnStartChunk()
    {
        var chunkGO = _pool.CreateStartChunk();
        _queue.Enqueue(chunkGO);
        _totalChunkCount++;
    }

    private void SpawnRandomChunk()
    {
        var chunkId = GetRandomChunkId(_prevChunkId);
        _prevChunkId = chunkId;
        var chunkGO = _pool.Get(chunkId);
        _queue.Enqueue(chunkGO);
        chunkGO.transform.position = new Vector3(0, 0, _chunkData.ChunkLength * _totalChunkCount);
        _totalChunkCount++;
    }

    private void DespawnChunk()
    {
        var chunk = _queue.Dequeue();
        _pool.Release(chunk);
    }

    private int GetRandomChunkId(int excludedChunkId)
    {
        var newChunkId = excludedChunkId;
        // Нужно избежать кейса чтобы друг за другом повторялся один и тот же кусок дороги
        while (newChunkId == excludedChunkId)
        {
            var id = Random.Range(0, _chunkData.Chunks.Count);
            newChunkId = _chunkData.Chunks[id].Id;
        }
        return newChunkId;
    }
}
