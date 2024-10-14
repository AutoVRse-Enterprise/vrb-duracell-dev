using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadlampNearHeadTriggerProperty : ScriptableActionInvoker
{

    public override void InvokeAction()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "I_Head_lamp")
        {
            StartCoroutine(IInvokeAction());
            other.transform.parent.parent.gameObject.SetActive(false);
        }
    }
    public override IEnumerator IInvokeAction()
    {
        onActionEnd?.Invoke();
        gameObject.SetActive(false);
        yield return null;
    }
}
