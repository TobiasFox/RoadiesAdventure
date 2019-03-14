using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkManager : MonoBehaviour
{
    [System.NonSerialized]
    public Camera cam;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        cam.gameObject.SetActive(false);
    }

    public void StartBlinking()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var blinkMat = transform.GetChild(i).GetComponent<BlinkingMaterial>();
            if (blinkMat != null)
            {
                blinkMat.StartBlinking();
            }
        }
    }

    public void StopBlinking()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<BlinkingMaterial>().StopBlinking();
        }
    }

}
