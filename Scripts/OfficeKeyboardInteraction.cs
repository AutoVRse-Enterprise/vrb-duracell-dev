using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRseBuilder.Core.NoCode;

public class OfficeKeyboardInteraction : ScriptableActionInvoker
{
    [SerializeField]
    private AudioSource _keyboardAudioSource;
    [SerializeField]
    private AudioClip _keyboardHitClip;
    [SerializeField]
    private TMPro.TMP_Text _screenText;
    [SerializeField]
    private float _delay = 10f, _keyboardHitDelayLowerLimit = 0.05f, _keyboardHitDelayUpperLimit = 0.5f;
    private bool _isHitOnce = false, _canPlay = true;
    private const string _characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private void OnCollisionEnter(Collision collision)
    {
        print("Collided with keyboard interaction!!");
        print("collided gameobject name: " + collision.gameObject.name);
        if ((collision.gameObject.CompareTag("LeftHand") || collision.gameObject.CompareTag("RightHand")) && _canPlay)
        {
            _isHitOnce = true;
            StartCoroutine(IPlayClipWithDelay());

        }
    }
    private IEnumerator IPlayClipWithDelay()
    {
        _canPlay = false;
        _screenText.text += _characters[Random.Range(0, _characters.Length)];
        _keyboardAudioSource.PlayOneShot(_keyboardHitClip);
        yield return new WaitForSeconds(Random.Range(_keyboardHitDelayLowerLimit, _keyboardHitDelayUpperLimit));
        _canPlay = true;
    }
    public override IEnumerator IInvokeAction()
    {
        float timer = 0;
        while (timer < _delay)
        {
            if (_isHitOnce)
            {
                timer += Time.deltaTime;
            }
            yield return null;
        }
    }
}
