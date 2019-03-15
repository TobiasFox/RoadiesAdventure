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
    private List<int> inputSequence = new List<int> { 0, 2, 1, 3, 2 };

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        cam.gameObject.SetActive(false);
        cam.enabled = true;

        foreach (var i in inputSequence)
        {
            Enum.TryParse("Alpha" + (i + 1), out KeyCode keyCode);
            inputKeyCodes.Add(keyCode);
        }
    }

    public IEnumerator SimpleRoutine()
    {
        yield return new WaitForSeconds(.5f);

        foreach (var i in inputSequence)
        {
            var blinkMat = transform.Find("keys").GetChild(i).GetComponent<BlinkingMaterial>();
            if (blinkMat != null)
            {
                blinkMat.StartBlinking();
                yield return new WaitForSeconds(.75f);
                blinkMat.StopBlinking();
            }
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

}
