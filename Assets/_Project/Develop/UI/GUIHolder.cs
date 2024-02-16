using UnityEngine;

/// <summary>
/// Простейший синглтон-компонент который хранит статическую ссылку на рут канвасом
/// </summary>
public class GUIHolder : MonoBehaviour
{
    /// <summary>
    /// Да, знаю что синглтоны это зло, но как рут для канвасом считаю приемлемым
    /// Логики в себе не несёт, и всё же лучше FindObjectOfType будет
    /// </summary>
    private static GUIHolder _instance;
    public static GUIHolder Instance => _instance;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("Duplicate GuiHolder Detected!");
        }
    }
}