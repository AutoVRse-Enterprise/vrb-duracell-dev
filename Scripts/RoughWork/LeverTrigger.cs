using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : ScriptableTriggerInvoker
{
    [SerializeField]
    private HingeJoint _hingeJoint;
    [SerializeField]
    private float _requiredAngle;
    public override IEnumerator IInvokeTrigger(){
        while (_hingeJoint.angle < _requiredAngle){
            yield return null;
        }
        yield return null;
    }
}
