using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectInstrument : MonoBehaviour
{
    private Instrument instrument = Instrument.Empty;
    private AudioSource[] audioSources;
    private int instrumentCount = 0;

    private void Awake()
    {
        audioSources = transform.GetChild(2).GetComponents<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tagName = collision.gameObject.tag;
        if (tagName.Equals("Instrument"))
        {
            if (collision.gameObject.name.Equals("synth"))
            {
                var blinkManager = collision.gameObject.GetComponent<BlinkManager>();
                blinkManager.cam.gameObject.SetActive(true);
                blinkManager.StartBlinking();
            }
            else
            {
                Enum.TryParse(collision.gameObject.name, out instrument);
                Destroy(collision.gameObject);
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
