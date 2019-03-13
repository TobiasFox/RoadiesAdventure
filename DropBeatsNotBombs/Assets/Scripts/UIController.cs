using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private float _crowdMoodTotalTimeInSeconds = 500;
    [SerializeField] private Slider _moodSlider;


    private float _bonusTime { get; set; }
    private Instrument _instrument = Instrument.Empty;

    private float _crowdMood;
    [SerializeField] private Image _inventoryImage; 
    [Tooltip("Use the same indexes as the Instrument enum: Empty, Drums, Bass, Synthesizer1, Synthesizer2")] [SerializeField] private Sprite[] _instrumentImages;

    // Start is called before the first frame update
    void Start()
    {
        _crowdMood = 1;
    }

    // Update is called once per frame
    void Update()
    {
        _crowdMood = (_crowdMoodTotalTimeInSeconds - Time.timeSinceLevelLoad + _bonusTime) / _crowdMoodTotalTimeInSeconds;
        _moodSlider.value = _crowdMood;

        //only for testing:
        
        //Adding bonus time
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            _bonusTime += 10;
        }
        //change inventory
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetNewInstrument((Instrument)Random.Range(0, _instrumentImages.Length));
        }
    }

    public void SetNewInstrument(Instrument instrument)
    {
        _instrument = instrument;
        _inventoryImage.sprite = _instrumentImages[(int)instrument];
    }
}
