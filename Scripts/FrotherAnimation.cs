using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class FrotherAnimation : ScriptableActionInvoker
{
    [SerializeField]
    private Transform _milkshakeFroth;
    [SerializeField]
    private PlacePoint _frotherPlacepoint;
    [SerializeField]
    private AudioSource _frothAudio;
    [SerializeField]
    private float _frothingSpeed, _speedRandomLowerLimit, _speedRandomUpperLimit;
    [SerializeField]
    private float _frothingSpeedChangeDelay;
    public void PlayFrotherAnimation()
    {
        StartCoroutine(IInvokeAction());
    }
    public override IEnumerator IInvokeAction()
    {
        _frothAudio.Play();
        StartCoroutine(RandomizeFrothingSpeed());
        while (_frothAudio.isPlaying)
        {
            _milkshakeFroth.eulerAngles += _frothingSpeed * Time.deltaTime * Vector3.up;
            yield return null;
        }
        _frotherPlacepoint.gameObject.SetActive(false);
        yield return null;
    }
    public IEnumerator RandomizeFrothingSpeed()
    {
        while (_frothAudio.isPlaying)
        {
            _frothingSpeed = Random.RandomRange(_speedRandomLowerLimit, _speedRandomUpperLimit);
            yield return new WaitForSeconds(_frothingSpeedChangeDelay);
        }
    }
}
