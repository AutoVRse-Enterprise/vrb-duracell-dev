using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ObjectMaterializerNearCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraObjectDisplayerPos;
    public float appearTime, disappearTime, uiAppearTime, uiDisappearTime, rotateSpeed;
    public bool _isObjectIntroducing;
    public Coroutine c_materializeObjectNearCamera, c_materializeObjectBackToPosition, c_rotateObject, c_materializeUI, c_demateralizeUI;
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    [Header("Debug")]
    public Transform testObj;
    public void MaterializeObjectNearCamera(PointedObjectData data)
    {
        print("MATERIALIZING OBJECT: " + data.pivotObject);
       c_materializeObjectNearCamera =  StartCoroutine(IBringObjectNearCamera(data));
    }
    public void MaterializeBattery(GameObject obj, GameObject teleportPos){
        obj.transform.SetPositionAndRotation(teleportPos.transform.position, teleportPos.transform.rotation);
        obj.transform.DOScale(Vector3.one, 0.5f);
    }
    public void DematerializeBattery(GameObject obj){
        obj.transform.DOScale(Vector3.zero, 0.5f);
    }
    public void MaterializeObjectAndBattery(PointedObjectData data){
        c_materializeObjectNearCamera = StartCoroutine(IMaterializeObjectAndBattery(data));
    }
    public void MaterializeUI(GameObject obj)
    {

        obj.SetActive(true);
        c_materializeUI = StartCoroutine(IMaterializeUI(obj));
    }
    public void DemateralizeUI(GameObject obj)
    {
        c_demateralizeUI = StartCoroutine(IDematerializeUI(obj));
    }
    public void MaterializeObjectInOriginalPosition(GameObject obj)
    {
        c_materializeObjectBackToPosition = StartCoroutine(IBringObjectToOriginalPosition(obj));
    }
    public void RotateObject(GameObject obj)
    {
        c_rotateObject = StartCoroutine(IRotateObject(obj));
    }
    public void StopRotatingObject()
    {
        if(c_rotateObject != null)
        {
            StopCoroutine(c_rotateObject);
        }
    }
    private IEnumerator IMaterializeUI(GameObject obj)
    {
        obj.transform.DOScale(Vector3.one, uiAppearTime);
        yield return new WaitForSeconds(uiAppearTime);
    }
    private IEnumerator IDematerializeUI(GameObject obj)
    {
        obj.transform.DOScale(Vector3.zero, uiDisappearTime);
        yield return new WaitForSeconds(uiDisappearTime);
        obj.SetActive(false);
    }
    private IEnumerator IMaterializeObjectAndBattery(PointedObjectData data){
        GameObject obj = data.pivotObject;
        _originalPosition = obj.transform.position;
        _originalRotation = obj.transform.rotation;
        Vector3 originalScale = obj.transform.localScale;
        obj.transform.DOScale(Vector3.zero, disappearTime);
        yield return new WaitForSeconds(disappearTime);
        obj.transform.SetPositionAndRotation(data.nearCameraTeleportPos.transform.position, data.nearCameraTeleportPos.transform.rotation);
        MaterializeBattery(data.batteryObject, data.batteryTeleportPos);
        obj.transform.DOScale(originalScale, appearTime);
        yield return new WaitForSeconds(appearTime);
        yield return null;
    }
    private IEnumerator IBringObjectNearCamera(PointedObjectData data)
    {
        GameObject obj = data.pivotObject;
        _originalPosition = obj.transform.position;
        _originalRotation = obj.transform.rotation;
        Vector3 originalScale = obj.transform.localScale;
        obj.transform.DOScale(Vector3.zero, disappearTime);
        yield return new WaitForSeconds(disappearTime);
        obj.transform.SetPositionAndRotation(data.nearCameraTeleportPos.transform.position, data.nearCameraTeleportPos.transform.rotation);
        obj.transform.DOScale(originalScale, appearTime);
        yield return new WaitForSeconds(appearTime);
        yield return null;
    }
    private IEnumerator IBringObjectToOriginalPosition(GameObject obj)
    {
        Vector3 originalScale = obj.transform.localScale;
        obj.transform.DOScale(Vector3.zero, disappearTime);
        yield return new WaitForSeconds(disappearTime);
        obj.transform.SetPositionAndRotation(_originalPosition, _originalRotation);
        obj.transform.DOScale(originalScale, appearTime);
        yield return null;
    }
    private IEnumerator IRotateObject(GameObject obj)
    {
        while (_isObjectIntroducing)
        {
            obj.transform.localEulerAngles += rotateSpeed * Time.deltaTime * Vector3.up;
            yield return null;
        }
    }
    
}
