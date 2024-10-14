using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsetSpawnPointLeveller : MonoBehaviour
{

    public Vector3 offset;
    private Transform[] _spawnPoints;
    private Quaternion localRotation;
    private float tempOffset = 0f;
    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<Transform>();
        localRotation = transform.localRotation;
 
    }
    //Camera.main here is supposed to be the main xr camera.
    private void LateUpdate()
    {
        transform.position = Camera.main.transform.position;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
    
    }
}
