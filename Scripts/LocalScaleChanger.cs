using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//x value remains at 0.0001
// y value goes from 0.00002 to 0.0002
public class LocalScaleChanger : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rect;
    [SerializeField]
    private Vector2 _localScale;
    private Vector3 _currentScale;
    void Start()
    {
        _currentScale = _rect.transform.localScale;
    }

    // Update is called once per frame
    private void OnValidate()
    {
        _currentScale.y = _localScale.y;
        _currentScale.x = _localScale.x;
        _rect.transform.localScale = _currentScale;
    }
}
