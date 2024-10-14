using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCCarSFXHandler : MonoBehaviour
{
    [System.Serializable]
    public class ClipSegmentData{
        public ClipSegments clipSegment;
        public float startTime;
        public float endTime;
    } 
    public enum ClipSegments {Start, Sustain, Stop}
    public ClipSegmentData[] clipSegments;
    public AudioSource audioSource;
    public float startTime = 10f;
    public float endTime = 20f;
    private Coroutine _coroutine;


    public void PlaySegment(float start, float end, bool loop = true)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
         _coroutine = StartCoroutine(LoopAudio(start, end, loop));
    }

    IEnumerator LoopAudio(float start, float stop, bool loop)
    {

        audioSource.loop = loop;
        if (audioSource.loop){
        while (audioSource.loop)
        {
            audioSource.time = startTime;
            audioSource.Play();
            while (audioSource.time < endTime)
            {
                yield return null;
            }
            audioSource.Stop();
            yield return null;
        }
        }
        else{
            audioSource.time = startTime;
            audioSource.Play();
            while (audioSource.isPlaying){
                if (audioSource.time >= endTime){
                    audioSource.Stop();
                    break;
                }
                yield return null;
            }
        }
        yield return null;

    }
}
