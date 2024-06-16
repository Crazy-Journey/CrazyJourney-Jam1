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

    public IEnumerator CameraShake(float shakeAmplitude = 1f, float shakeIntensity = 3f, float shakeTiming = 0.2f)
    {
        Noise(shakeAmplitude, shakeIntensity);
        yield return new WaitForSeconds(shakeTiming);
        Noise(0, 0);
    }

    public void Noise(float amplitudeGain, float frequencyGain)
    {
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeGain;
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequencyGain;
    }

    private void Update()
    {
        int pisoP1 = PlayerDataManager.THIS.GetPlayer(0).GetPiso();
        int pisoP2 = PlayerDataManager.THIS.GetPlayer(1).GetPiso();

        float zoomTarget = 7;

        
        
        if (Mathf.Abs(pisoP1 - pisoP2) == 0) zoomTarget = 7;
        if (Mathf.Abs(pisoP1 - pisoP2) == 1) zoomTarget = 8;
        if (Mathf.Abs(pisoP1 - pisoP2) == 2) zoomTarget = 9.5f;
        if (Mathf.Abs(pisoP1 - pisoP2) == 3) zoomTarget = 12f;

        float currentVel = 0.0f;
        vCam.m_Lens.OrthographicSize = Mathf.SmoothDamp(vCam.m_Lens.OrthographicSize, zoomTarget, ref currentVel, 0.05f);
    }
}
