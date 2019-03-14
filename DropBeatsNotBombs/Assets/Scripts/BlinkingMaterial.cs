using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingMaterial : MonoBehaviour
{
    private Renderer rend;
    private float blinkRythm = .5f;
    private bool startBlinking = false;
    public Material material1;
    public Material material2;
    float duration = .5f;

    void Awake()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

    public void StartBlinking()
    {
        //StartCoroutine("Blink");
        startBlinking = true;
    }

    public void StopBlinking()
    {
        //StopCoroutine("Blink");
        startBlinking = false;
        rend.material = material1;
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

    private void Update()
    {
        if (!startBlinking)
        {
            return;
        }

        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.Lerp(material2, material1, lerp);
    }

}
