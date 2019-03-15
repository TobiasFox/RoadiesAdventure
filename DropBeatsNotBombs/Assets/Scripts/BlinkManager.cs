using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkManager : MonoBehaviour
{
    [System.NonSerialized]
    public Camera cam;

    private bool _inputRequest = false;
    private List<KeyCode> _inputKeyCodes = new List<KeyCode>();
    private int _currentButton = 0;
    private List<List<int>> _inputSequence = new List<List<int>> { new List<int>() { 0, 2, 1, 3, 2 }, new List<int>() { 3, 0, 2, 1, 3 } };
    private AudioManager _audioManager;
    private Transform _keys;
    private int _inputSequenceIndex = 0;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        cam = GetComponentInChildren<Camera>();
        cam.gameObject.SetActive(false);
        cam.enabled = true;
        _keys = transform.Find("keys");
        _inputSequenceIndex = Int32.Parse(name.Substring(name.Length - 1)) - 1;

        foreach (var i in _inputSequence[_inputSequenceIndex])
        {
            Enum.TryParse("Alpha" + (i + 1), out KeyCode keyCode);
            _inputKeyCodes.Add(keyCode);
        }
    }

    private IEnumerator PlayAndShowKeyboardTone(int key)
    {
        var _blinkMat = _keys.GetChild(key).GetComponent<BlinkingMaterial>();
        if (_blinkMat != null)
        {
            _audioManager.Play("SynthPuzzle1_" + key);
            _blinkMat.StartBlinking();
            yield return new WaitForSeconds(.6f);
            _blinkMat.StopBlinking();
        }
    }

    public IEnumerator SimpleRoutine()
    {
        yield return new WaitForSeconds(.5f);

        foreach (var i in _inputSequence[_inputSequenceIndex])
        {
            yield return PlayAndShowKeyboardTone(i);
        }

        _inputRequest = true;
        Debug.Log("start puzzle, wait for: " + _inputKeyCodes[_currentButton]);
    }

    public void StartBlinking()
    {
        StartCoroutine("SimpleRoutine");
    }

    private void Update()
    {
        if (!_inputRequest)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            Debug.Log("show sequence again.");
            _inputRequest = false;
            _currentButton = 0;
            StartCoroutine("SimpleRoutine");
            return;
        }

        PlayKeyboardSound();

        if (Input.GetKeyUp(_inputKeyCodes[_currentButton]))
        {
            _currentButton++;

            if (_currentButton >= _inputKeyCodes.Count)
            {
                Debug.Log("finish puzzle");
                _inputRequest = false;

                Enum.TryParse(gameObject.name, out Instrument instrument);
                FindObjectOfType<CollectInstrument>().AddInstrument(instrument);
                Destroy(gameObject);
                return;
            }
            Debug.Log(" wait for next input from " + _inputKeyCodes[_currentButton]);
        }
        else
        {
            if (Input.anyKey && !Input.GetKey(_inputKeyCodes[_currentButton]))
            {
                _currentButton = 0;
                Debug.Log("reset puzzle, next input from " + _inputKeyCodes[_currentButton]);
            }
        }
    }

    private void PlayKeyboardSound()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine("PlayAndShowKeyboardTone", 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine("PlayAndShowKeyboardTone", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine("PlayAndShowKeyboardTone", 2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine("PlayAndShowKeyboardTone", 3);
        }
    }
}
