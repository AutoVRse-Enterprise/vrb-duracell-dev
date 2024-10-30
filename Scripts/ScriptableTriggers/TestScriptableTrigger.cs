using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestScriptableTrigger : ScriptableActionInvoker
{
    public float time;
    public Ease ease;
    public void PlayAnim(){
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, time).SetEase(ease);
    }
    public override IEnumerator IInvokeAction(){
                transform.DOScale(Vector3.one, time).SetEase(Ease.InOutElastic);
                yield return new WaitForSeconds(time);
    }
}
