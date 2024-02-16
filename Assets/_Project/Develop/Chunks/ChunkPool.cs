using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ChunkPool
{
    private ChunkData _data;
    private List<Chunk> _createdChunks;
    private Transform _root;
    private DiContainer _diContainer;

    public ChunkPool(ChunkData data, DiContainer diContainer, Transform root)
    {
        _data = data;
        _root = root;
        _createdChunks = new List<Chunk>();
        _diContainer = diContainer;
    }

    public Chunk Get(int id)
    {
        var obj = _createdChunks.FirstOrDefault(x => !x.isActiveAndEnabled && x.Id == id);

        if (obj == null)
        {
            obj = Create(id);
        }

        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Release(Chunk obj)
    {
        obj.gameObject.SetActive(false);
    }

    private Chunk Create(int id)
    {
        var prefab = _data.Chunks.FirstOrDefault(x => x.Id == id);
        if(prefab == null)
        {
            Debug.LogErrorFormat("can't find chunk with id {0}", id);
            return null;
        }

        var obj = GameObject.Instantiate(prefab, _root);
        _diContainer.Inject(obj);
        _createdChunks.Add(obj);
        return obj;
    }

    /// <summary>
    /// Важно стартовый чанк не хранить в пуле так как он должен существовать только на старте игры
    /// </summary>
    /// <returns></returns>
    public Chunk CreateStartChunk()
    {
        var prefab = _data.StartChunk;
        var obj = GameObject.Instantiate(prefab, _root);
        _diContainer.Inject(obj);
        return obj;
    }
}