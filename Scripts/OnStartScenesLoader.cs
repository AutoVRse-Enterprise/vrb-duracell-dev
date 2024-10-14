using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnStartScenesLoader : MonoBehaviour
{
    [SerializeField]
    [ScenePath]
    private string _initializerScene, _artSceneName, _devSceneName;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(ILoadScenes());
    }
    private IEnumerator ILoadScenes(){
        AsyncOperation op = SceneManager.LoadSceneAsync(_artSceneName, LoadSceneMode.Single);
        while (!op.isDone){
            yield return null;
        }
        op = SceneManager.LoadSceneAsync(_devSceneName,LoadSceneMode.Additive);
        while(!op.isDone){
            yield return null;
        }
        Destroy(gameObject);
    }
}
