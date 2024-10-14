using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRseBuilder.Core.Utility;

public class ChangeSceneWithFadeProperty : ScriptableActionInvoker
{
    [SerializeField]
    private FadeController _fadeController;
    [SerializeField]
    private string _mainSceneName, _artSceneName;
    [SerializeField]
    private float _fadeTime;
    private void Start()
    {
    }
    public override IEnumerator IInvokeAction()
    {
        _fadeController.FadeIn(_fadeTime);
        yield return new WaitForSeconds(_fadeTime);
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
        AsyncOperation op;
        op = SceneManager.LoadSceneAsync(_artSceneName, LoadSceneMode.Single);
        while (!op.isDone)
        {
            yield return null;
        }
        op = SceneManager.LoadSceneAsync(_mainSceneName, LoadSceneMode.Additive);
        while (!op.isDone)
        {
            yield return null;
        }
        GameObject masterRig = GameObject.Find("MasterRig");
        _fadeController = masterRig.GetComponentInChildren<FadeController>(true);
        _fadeController.FadeOut(_fadeTime);
        yield return new WaitForSeconds(_fadeTime);
        onActionEnd?.Invoke();
        Destroy(gameObject);
    }
}
