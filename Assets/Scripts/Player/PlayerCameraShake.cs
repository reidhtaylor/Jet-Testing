using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noise;

    private Coroutine process;
    private float t;

    private void Start() {
        vcam = GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float power, float speed, float time) {
        if (MapBounds.instance == null) {
            Debug.Log("No MapBounds instance exists which is used to start Coroutines on this Object", this);
            return;
        }
        if (process != null) StopAllCoroutines();
        process = MapBounds.instance.StartCoroutine(ProcessShake(power, speed, time));
    }
    private IEnumerator ProcessShake(float power, float speed, float time) {
        SetNoise(power, speed);
        t = 0;
        while (t < time) {
            t += Time.deltaTime;
            yield return null;
        }
        SetNoise(0, 0);
    }

    private void SetNoise(float amp, float freq) {
        noise.m_AmplitudeGain = amp;
        noise.m_FrequencyGain = freq; 
    }
}
