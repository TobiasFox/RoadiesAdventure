using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingMaterial : MonoBehaviour
{
    private Renderer rend;
    private bool startBlinking = false;
    public Material material1;
    public Material material2;
    float duration = .3f;
    private static float startTime;

    void Awake()
    {
        rend = gameObject.GetComponent<Renderer>();
        startTime = Time.time;
    }

    public void StartBlinking()
    {
        startBlinking = true;
    }

    public void StopBlinking()
    {
        startBlinking = false;
        rend.material = material2;
    }

    private void Update()
    {
        if (!startBlinking)
        {
            return;
        }

        float lerp = Mathf.PingPong(startTime - Time.time, duration) / duration;
        rend.material.Lerp(material1, material2, lerp);
    }

}
