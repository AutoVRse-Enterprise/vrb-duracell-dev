﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtPlayer : MonoBehaviour
{ 
  private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
