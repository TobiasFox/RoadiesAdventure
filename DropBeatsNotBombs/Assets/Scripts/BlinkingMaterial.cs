using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingMaterial : MonoBehaviour
{
    private Renderer _rend;
    private bool _startBlinking = false;
    public Material _material1;
    public Material _material2;
    float _duration = .3f;
    private static float START_TIME;

    void Awake()
    {
        _rend = gameObject.GetComponent<Renderer>();
        START_TIME = Time.time;
    }

    public void StartBlinking()
    {
        _startBlinking = true;
    }

    public void StopBlinking()
    {
        _startBlinking = false;
        _rend.material = _material2;
    }

    private void Update()
    {
        if (!_startBlinking)
        {
            return;
        }

        float lerp = Mathf.PingPong(START_TIME - Time.time, _duration) / _duration;
        _rend.material.Lerp(_material1, _material2, lerp);
    }

}
