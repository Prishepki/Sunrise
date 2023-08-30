using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldRoom : MonoBehaviour
{
    public static WorldRoom Singleton;

    [field: SerializeField] public Passage[] Passages { get; private set; }

    private void Awake() => Singleton = this;

    [ContextMenu("Detect Passages")]
    public void DetectPassages()
    {   
        Passages = FindObjectsOfType<Passage>(true);
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
    }
}
