using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRToggler : MonoBehaviour
{
    [SerializeField]
    private OVRManager _ovrManager;
    [SerializeField]
    private OVRPassthroughLayer _oVRPassthroughLayer;
    [SerializeField]
    private Material _originalSkyboxMat;
    [SerializeField]
    private bool _switchedToMR = true, _useSkybox = false;
    private void OnEnable()
    {
        print("enabled");

    }
    public void SwitchToMR()
    {
        _ovrManager.isInsightPassthroughEnabled = true;
        _ovrManager.enableMixedReality = true;
        _oVRPassthroughLayer.textureOpacity = 1;
        RenderSettings.skybox = null;
        print("RenderSettings.skybox: " + RenderSettings.skybox);
    }
    public void SwitchToVR()
    {
        _ovrManager.isInsightPassthroughEnabled = false;
        _ovrManager.enableMixedReality = false;
        _oVRPassthroughLayer.textureOpacity = 0;
        print("OriginalSkyboxMat: " + _originalSkyboxMat);
        if (_useSkybox)
        {
            RenderSettings.skybox = _originalSkyboxMat;
            print("RenderSettings.skybox: " + RenderSettings.skybox);
            DynamicGI.UpdateEnvironment();
        }
    }
}
