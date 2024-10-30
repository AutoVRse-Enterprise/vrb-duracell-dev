using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadArtSceneAction : ScriptableActionInvoker
{
    [SerializeField]
    [ScenePath]
    private string _artScene;
    [SerializeField]
    private GameObject _loadingScreen;
    [SerializeField]
    private Image _fillPanel;
    [SerializeField]
    private float _interpolationSpeed, _waitAfterLoad;
    public override IEnumerator IInvokeAction(){
        AsyncOperation op =  SceneManager.LoadSceneAsync(_artScene, LoadSceneMode.Additive);
        //_loadingScreen.SetActive(true);
        float targetFill;
        while(!op.isDone){
               // targetFill = op.progress/0.9f;
                //_fillPanel.fillAmount = Mathf.Lerp(_fillPanel.fillAmount, targetFill, Time.deltaTime * _interpolationSpeed);
            
            yield return null;
        }
        //_loadingScreen.SetActive(false);
       // yield return new WaitForSeconds(_waitAfterLoad);
    }
}
