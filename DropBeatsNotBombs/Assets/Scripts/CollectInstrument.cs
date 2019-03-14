using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectInstrument : MonoBehaviour
{
    private Instrument instrument = Instrument.Empty;
    private AudioSource[] audioSources;
    private int instrumentCount = 0;
    private List<Instrument> instrumentsList = new List<Instrument>();

    private void Awake()
    {
        audioSources = transform.GetChild(2).GetComponents<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        string tagName = other.gameObject.tag;
        if (tagName.Equals("Instrument"))
        {
            if (other.gameObject.name.Equals("synth"))
            {
                var blinkManager = other.gameObject.GetComponent<BlinkManager>();
                blinkManager.cam.gameObject.SetActive(true);
                blinkManager.StartBlinking();
            }
            else
            {
                Enum.TryParse(other.gameObject.name, out instrument);
                instrumentsList.Add(instrument);
                Destroy(other.gameObject);
                Debug.Log("add instrument " + instrument.ToString());
            }
        }

        if (tagName.Equals("Delivery_Point"))
        {
            if (instrument != Instrument.Empty)
            {
                audioSources[(int)instrument].mute = false;
                instrumentCount++;

                if (instrumentCount >= 4)
                {
                    audioSources[0].mute = false;
                }

                instrument = Instrument.Empty;
            }
        }
    }

}
