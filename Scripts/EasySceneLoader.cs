using System.Collections;
using System.Collections.Generic;
using Fusion;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
public class EasySceneLoader : MonoBehaviour
{
    [ScenePath]
    [SerializeField]
    private string[] _devScenes;
    [ScenePath]
    [SerializeField]
    private string[] _artScene;
    [ScenePath]
    [SerializeField]
    private string _menuScene;
    [SerializeField]
    private int _devSceneIndex, _artSceneIndex;

    public void LoadArtAndDevScene()
    {
#if UNITY_EDITOR
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene(_artScene[_artSceneIndex], OpenSceneMode.Single);
        EditorSceneManager.OpenScene(_devScenes[_devSceneIndex], OpenSceneMode.Additive);
#endif
    }
    public void LoadMenuScene()
    {
#if UNITY_EDITOR
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene(_menuScene);
#endif
    }
    public void LoadOnlyDevScene()
    {
#if UNITY_EDITOR
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene(_devScenes[_devSceneIndex]);
#endif
    }
    public void LoadOnlyArtScene()
    {
#if UNITY_EDITOR
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene(_artScene[_artSceneIndex]);
#endif
    }
}
