using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForPointerPressScript : ScriptableActionInvoker
{
    [SerializeField]
    private HandheldRemotePointer _handheldRemotePointer;
    public override IEnumerator IInvokeAction()
    {
        while (!_handheldRemotePointer.isPointing)
        {
            yield return null;
        }
    }
}
