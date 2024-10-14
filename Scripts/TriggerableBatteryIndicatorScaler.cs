using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableBatteryIndicatorScaler : ScriptableActionInvoker
{
    [SerializeField]
    private RectTransform _rect;
    [SerializeField]
    private Vector2 _localScale;
    private Vector3 _currentScale;
    [SerializeField]
    private float _finalYScale = 0.00017f;
    [SerializeField]
    private float waitForCompleteDelay;
    private Vector3 initialPosition;
    void Start()
    {
        _currentScale = _rect.transform.localScale;
        initialPosition = _rect.transform.position;
    }
    public void TempFunc()
    {
        StartCoroutine(IInvokeAction());
    }
    public override IEnumerator IInvokeAction()
    {
        float timer = 0;
        float currentYScale = _rect.transform.localScale.y;
        while (timer < waitForCompleteDelay)
        {
            timer += Time.deltaTime;
            _localScale.y = Mathf.Lerp(currentYScale, _finalYScale, timer / waitForCompleteDelay);
            if (_rect.pivot.y == 0.5f) // If the pivot is in the center
            {
                float scaleDifference = _localScale.y - _rect.transform.localScale.y;
                _rect.transform.position = new Vector3(
                    _rect.transform.position.x,
                    initialPosition.y + (scaleDifference / 2),
                    _rect.transform.position.z);
            }

            // Apply the new scale
            _rect.transform.localScale = _localScale;
            yield return null;
        }
    }
}
