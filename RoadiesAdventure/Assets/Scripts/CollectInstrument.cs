using Invector.CharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectInstrument : MonoBehaviour
{
    [SerializeField] private int _instrumentsCountToWin = 4;

    private Instrument _instrument = Instrument.Empty;
    private AudioSource[] _audioSources;
    private int _instrumentCount = 0;
    private AudioManager _audioManager;
    [SerializeField] private float _bonusTimeForInstrument = 60;
    private UIController _uIController;
    private DialogManager _dialogManager;

    private void Awake()
    {
        var _audioGO = transform.parent.Find("AudioSources");
        _audioSources = _audioGO.GetComponents<AudioSource>();
        _audioManager = FindObjectOfType<AudioManager>();
        _uIController = FindObjectOfType<UIController>();
        _dialogManager = FindObjectOfType<DialogManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var _tagName = other.gameObject.tag;

        if (_tagName.Equals("Instrument"))
        {
            if (_instrument != Instrument.Empty)
            {
                Dialog _pickUpInstrumentDialog = new Dialog("You already carry an instrument. Bring it back to the stage before you can carry the next one.");
                _dialogManager.StartDielogue(_pickUpInstrumentDialog);
                return;
            }

            if (other.gameObject.name.Equals("Synthesizer1") || other.gameObject.name.Equals("Synthesizer2"))
            {
                var _blinkManager = other.transform.parent.gameObject.GetComponent<BlinkManager>();
                _blinkManager.cam.gameObject.SetActive(true);
                _blinkManager.StartBlinking();
                other.gameObject.SetActive(false);

                GetComponent<vThirdPersonInput>().ResetAndLockMovement(true);
            }
            else if (other.gameObject.name.Equals("Drums"))
            {
                other.GetComponent<Drums>().ShowUI();
                Cursor.lockState = CursorLockMode.None;
                gameObject.GetComponent<vThirdPersonInput>().MouseLocked = true;
                Cursor.visible = true;
                gameObject.GetComponent<vThirdPersonInput>().ResetAndLockMovement(true);
            }
            else if (other.gameObject.name.Equals("Bass"))
            {
                other.GetComponent<Bass>().ShowUI();
                Cursor.lockState = CursorLockMode.None;
                gameObject.GetComponent<vThirdPersonInput>().MouseLocked = true;
                Cursor.visible = true;
                gameObject.GetComponent<vThirdPersonInput>().ResetAndLockMovement(true);
            }
        }

        if (_tagName.Equals("Delivery_Point"))
        {
            if (_instrument != Instrument.Empty)
            {
                _audioSources[(int)_instrument].mute = false;
                _instrumentCount++;
                _uIController._bonusTime += _bonusTimeForInstrument;
                _uIController.SetNewInstrument(Instrument.Empty);
                other.GetComponent<DeliveryPoint>().ActivateInstrument(_instrument);

                if (_instrumentCount >= _instrumentsCountToWin)
                {
                    Win();
                }

                _instrument = Instrument.Empty;
            }
        }
    }

    private void Win()
    {
        _audioSources[0].mute = false;
        _audioManager.Play("Applause");
        _uIController.PlayWinParticles();
        _uIController.ShowWinButoon();
        //TODO geht auch schöner! (refactoring)
        Dialog _winDialog = new Dialog("You made it! The croud is amused and you made it possible. Thank you so much!");
        _dialogManager.StartDielogue(_winDialog);
    }

    public void AddInstrument(Instrument instrument)
    {
        this._instrument = instrument;
        Debug.Log("add instrument " + instrument.ToString());
        _uIController.SetNewInstrument(instrument);

        if (instrument == Instrument.Synthesizer1 || instrument == Instrument.Synthesizer2)
        {
            GetComponent<vThirdPersonInput>().ResetAndLockMovement(false);
        }
    }

}
