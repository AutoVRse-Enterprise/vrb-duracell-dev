using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using VRseBuilder.Core.NoCode;
using VRseBuilder.Core.Player;

public class CustomTriggerTeleportProperty : ScriptableActionInvoker
{
    [SerializeField]
    private XRPlayer _player;
    [SerializeField]
    private float waitForCompleteDelay = 1f;
    public override IEnumerator IInvokeAction()
    {
        _player.Teleport(transform, true, waitForCompleteDelay);
        yield return new WaitForSeconds(waitForCompleteDelay);
    }
}
