using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PoweredObjectsListHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _panels;
    [System.Serializable]
    private class PanelData
    {
        [HideInInspector]
        public Transform untickImage;
        [HideInInspector]
        public Transform tickImage;
        [HideInInspector]
        public TMP_Text panelText;
        public string objectName;
        public GameObject associatedObject;
        public Transform panelLocation;
    }
    [SerializeField]
    private PanelData[] _panelData;
    [Header("Debugging")]
    public int index;
    private void Start()
    {
        for (int i = 0; i < _panels.Length; i++)
        {
            _panelData[i].tickImage = _panels[i].transform.GetChild(0);
            _panelData[i].untickImage = _panels[i].transform.GetChild(1);
            _panelData[i].panelText = _panels[i].transform.GetChild(2).GetComponent<TMP_Text>();
        }
    }
    public void TickMarkPanel(int index)
    {
        _panelData[index].untickImage.gameObject.SetActive(false);
        _panelData[index].tickImage.gameObject.SetActive(true);
        _panelData[index].panelText.text = _panelData[index].objectName;
    }
    public void UnTickMarkPanel(int index)
    {
        _panelData[index].untickImage.gameObject.SetActive(true);
        _panelData[index].tickImage.gameObject.SetActive(false);
        _panelData[index].panelText.text = "???";
    }
    public void TickMarkPanel(GameObject obj)
    {
        foreach(PanelData panelData in _panelData)
        {
            if (obj == panelData.associatedObject)
            {
                panelData.untickImage.gameObject.SetActive(false);
                panelData.tickImage.gameObject.SetActive(true);
                panelData.panelText.text = panelData.objectName;
            }
        }
    }
    public void SetPanelLocation(int index)
    {
        transform.SetPositionAndRotation(
            _panelData[index].panelLocation.position,
            _panelData[index].panelLocation.rotation);
    }
}
