using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBatteryInsertAfterTouchProperty : ScriptableActionInvoker
{
    [SerializeField]
    private Autohand.AutoHandPlayer _player;
    public bool enableCollisionDetection = false;
    public void ToggleCollisionDetection(bool val)
    {
        enableCollisionDetection = val;
    }
    public override void InvokeAction(){

    }
    private void OnCollisionEnter(Collision collision)
    {
        print ("Entered Collision detection for after touch property.");
        print ("Collider is: " + collision.gameObject.name);
        print ("enable collision detection: " + enableCollisionDetection);
        if (enableCollisionDetection)
        {
            GameObject grabbedObj;
            if (collision.gameObject.CompareTag("Battery"))
            {
                if (_player.handLeft.IsGrabbing())
                {
                    grabbedObj = _player.handLeft.holdingObj.gameObject;
                    _player.handLeft.Release();
                    grabbedObj.SetActive(false);
                }
                else if (_player.handRight.IsGrabbing())
                {
                    grabbedObj = _player.handRight.holdingObj.gameObject;
                    _player.handRight.Release();
                    grabbedObj.SetActive(false);
                }
                collision.gameObject.SetActive(false);
                onActionEnd?.Invoke();
            }
        }
    }
}
