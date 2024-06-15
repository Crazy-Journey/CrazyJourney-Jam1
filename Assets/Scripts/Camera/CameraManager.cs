using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Para usar el camera shake, enchufamos la siguiente linea:
//
// StartCoroutine(Camera.main.GetComponent<CameraManager>().CameraShake());
//

public class CameraManager : MonoBehaviour
{ 
    [SerializeField] private CinemachineVirtualCamera vCam;

    public IEnumerator CameraShake(float shakeIntensity = 3f, float shakeTiming = 0.2f)
    {
        Noise(1, shakeIntensity);
        yield return new WaitForSeconds(shakeTiming);
        Noise(0, 0);
    }

    public void Noise(float amplitudeGain, float frequencyGain)
    {
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeGain;
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequencyGain;
    }
}
