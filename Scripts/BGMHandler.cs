using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMHandler : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;
    [SerializeField]
    private float _fadeSpeed, _defaultVolume;
    public static BGMHandler instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else 
        {
            Destroy(this);
        }
    }
    public void PlayBGM(){
        _source.volume = _defaultVolume;
        _source.Play();
    }
    public void StopBGM(){
        StartCoroutine(IStopBGM());
    }
    private IEnumerator IStopBGM(){
        while (_source.volume > 0){
            _source.volume -= (_fadeSpeed * Time.deltaTime);
            yield return null;
        }
        _source.Stop();
    }
}
