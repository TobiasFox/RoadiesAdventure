using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShow : MonoBehaviour
{
    private Light[] _lights;
    private int _currentLight = 0;
    private float _startLight;
    private float _timeSwitch;
    private float _beats = 0;
    private bool _switchLight = true;
    private float _switchRandomLightsInterval;

    public float maxTime;
    public bool nextRandomLight;


    void Awake()
    {
        _lights = GetComponentsInChildren<Light>();
        _startLight = _lights[_currentLight].intensity;
        _timeSwitch = Time.time + maxTime;
        _switchRandomLightsInterval = Time.time + 16 * maxTime;
    }

    void Update()
    {
        if (Time.time >= _timeSwitch)
        {
            if (_beats < 4)
            {
                _lights[_currentLight].intensity = _switchLight ? 0 : _startLight;
                _switchLight = !_switchLight;
                _timeSwitch += maxTime;
                _beats++;
            }
            else
            {
                if (nextRandomLight)
                {
                    var nextLight = 0;
                    do
                    {
                        nextLight = Random.Range(0, 4);
                    } while (nextLight == _currentLight);
                    _currentLight = nextLight;
                }
                else
                {
                    _currentLight = (_currentLight + 1) % _lights.Length;
                }

                if (Time.time > _switchRandomLightsInterval)
                {
                    nextRandomLight = !nextRandomLight;
                    _switchRandomLightsInterval += 16 * maxTime;
                }

                _beats = 0;
                _startLight = _lights[_currentLight].intensity;
            }
        }
    }
}
