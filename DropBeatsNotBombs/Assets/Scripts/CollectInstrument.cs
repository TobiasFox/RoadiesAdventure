using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectInstrument : MonoBehaviour
{
    [SerializeField] private int _instrumentsCountToWin = 4;

    private Instrument instrument = Instrument.Empty;
    private AudioSource[] audioSources;
    private int instrumentCount = 0;
    private List<Instrument> instrumentsList = new List<Instrument>();
    private AudioManager _audioManager;
    [SerializeField] private float _bonusTimeForInstrument = 60;
    private UIController uIController;
    private DialogManager dialogManager;

    private void Awake()
    {
        var audioGO = transform.parent.Find("AudioSources");
        audioSources = audioGO.GetComponents<AudioSource>();
        _audioManager = FindObjectOfType<AudioManager>();
        uIController = FindObjectOfType<UIController>();
        dialogManager = FindObjectOfType<DialogManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        string tagName = other.gameObject.tag;
        if (tagName.Equals("Instrument"))
        {
            if (instrument != Instrument.Empty)
            {
                Dialog gameOverDialog = new Dialog("You already carry an instrument. Bring it back to the stage before you can carry the next one.");
                dialogManager.StartDielogue(gameOverDialog);
                return;
            }

            if (other.gameObject.name.Equals("synth"))
            {
                var blinkManager = other.gameObject.GetComponent<BlinkManager>();
                blinkManager.cam.gameObject.SetActive(true);
                blinkManager.StartBlinking();
            }
            else
            {
                Enum.TryParse(other.transform.parent.name, out instrument);
                instrumentsList.Add(instrument);
                Destroy(other.transform.parent.gameObject);
                Debug.Log("add instrument " + instrument.ToString());
                uIController.SetNewInstrument(instrument);
            }
        }

        if (tagName.Equals("Delivery_Point"))
        {
            if (instrument != Instrument.Empty)
            {
                audioSources[(int)instrument].mute = false;
                instrumentCount++;
                uIController._bonusTime += _bonusTimeForInstrument;
                uIController.SetNewInstrument(Instrument.Empty);

                if (instrumentCount >= _instrumentsCountToWin)
                {
                    Win();
                }

                instrument = Instrument.Empty;
            }
        }


    }

    private void Win()
    {
        audioSources[0].mute = false;
        _audioManager.Play("Applause");
        //TODO geht auch schöner! (refactoring)
        Dialog gameOverDialog = new Dialog("You made it! The croud is amused and you made it possible. Thank you so much!");
        dialogManager.StartDielogue(gameOverDialog);
    }

}
