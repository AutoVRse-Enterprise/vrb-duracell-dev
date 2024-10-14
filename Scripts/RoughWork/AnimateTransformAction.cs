using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Diagnostics;
public class AnimateTransformAction : ScriptableActionInvoker{

  public override IEnumerator IInvokeAction(){
    print("INSIDE THE FUNCTION!!");
    yield return new WaitForSeconds(5);
    print("5 seconds finished!!");
    yield return null;
  }
}
