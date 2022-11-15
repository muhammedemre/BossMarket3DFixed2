using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofelt.NiceVibrations;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager instance;
    [SerializeField] bool hapticsSupported;

    private void Awake()
    {
        SingletonCheck();
        hapticsSupported = DeviceCapabilities.isVersionSupported;
    }

    void SingletonCheck()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public void PlayWarningVibration()
    {
        if (UIManager.instance.settingsMenuActor.vibrationState)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
        }
        
    }
    public void PlaySoftVibration()
    {
        if (UIManager.instance.settingsMenuActor.vibrationState)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
        }
        
    }

}
