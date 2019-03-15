using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkManager : MonoBehaviour
{
    [System.NonSerialized]
    public Camera cam;

    private bool inputRequest = false;
    private List<KeyCode> inputKeyCodes = new List<KeyCode>();
    private int currentButton = 0;
    private List<List<int>> inputSequence = new List<List<int>> { new List<int>() { 0, 2, 1, 3, 2 }, new List<int>() { 3, 0, 2, 1, 3 } };
    private AudioManager audioManager;
    private Transform keys;
    private int inputSequenceIndex = 0;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        cam = GetComponentInChildren<Camera>();
        cam.gameObject.SetActive(false);
        cam.enabled = true;
        keys = transform.Find("keys");
        inputSequenceIndex = Int32.Parse(name.Substring(name.Length - 1)) - 1;

        foreach (var i in inputSequence[inputSequenceIndex])
        {
            Enum.TryParse("Alpha" + (i + 1), out KeyCode keyCode);
            inputKeyCodes.Add(keyCode);
        }
    }

    private IEnumerator PlayAndShowKeyboardTone(int key)
    {
        var blinkMat = keys.GetChild(key).GetComponent<BlinkingMaterial>();
        if (blinkMat != null)
        {
            audioManager.Play("SynthPuzzle1_" + key);
            blinkMat.StartBlinking();
            yield return new WaitForSeconds(.6f);
            blinkMat.StopBlinking();
        }
    }

    public IEnumerator SimpleRoutine()
    {
        yield return new WaitForSeconds(.5f);

        foreach (var i in inputSequence[inputSequenceIndex])
        {
            yield return PlayAndShowKeyboardTone(i);
        }

        inputRequest = true;
        Debug.Log("start puzzle, wait for: " + inputKeyCodes[currentButton]);
    }

    public void StartBlinking()
    {
        StartCoroutine("SimpleRoutine");
    }

    private void Update()
    {
        if (!inputRequest)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            Debug.Log("show sequence again.");
            inputRequest = false;
            currentButton = 0;
            StartCoroutine("SimpleRoutine");
            return;
        }

        PlayKeyboardSound();

        if (Input.GetKeyUp(inputKeyCodes[currentButton]))
        {
            currentButton++;

            if (currentButton >= inputKeyCodes.Count)
            {
                Debug.Log("finish puzzle");
                inputRequest = false;

                Enum.TryParse(gameObject.name, out Instrument instrument);
                FindObjectOfType<CollectInstrument>().AddInstrument(instrument);
                Destroy(gameObject);
                return;
            }
            Debug.Log(" wait for next input from " + inputKeyCodes[currentButton]);
        }
        else
        {
            if (Input.anyKey && !Input.GetKey(inputKeyCodes[currentButton]))
            {
                currentButton = 0;
                Debug.Log("reset puzzle, next input from " + inputKeyCodes[currentButton]);
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
