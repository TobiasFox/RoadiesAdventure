using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingMaterial : MonoBehaviour
{
    private Renderer rend;
    private float blinkRythm = .5f;

    void Awake()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

    public void StartBlinking()
    {
        StartCoroutine("Blink");
    }

    public void StopBlinking()
    {
        StopCoroutine("Blink");
    }

    public IEnumerator Blink()
    {
        while (true)
        {
            rend.enabled = false;
            yield return new WaitForSeconds(blinkRythm);
            rend.enabled = true;
            yield return new WaitForSeconds(blinkRythm);
        }
    }

}
