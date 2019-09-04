using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    GameObject[] instruments = new GameObject[4];

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var go = transform.GetChild(i).gameObject;
            instruments[i] = go;
            go.SetActive(false);
        }
    }

    public void ActivateInstrument(Instrument instrument)
    {
        Debug.Log("activate " + instrument.ToString() + " " + (int)instrument);
        instruments[((int)instrument) - 1].SetActive(true);
    }
}
