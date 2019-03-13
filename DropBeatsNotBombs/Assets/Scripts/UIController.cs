using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private float _crowdMoodTotalTimeInSeconds = 500;
    [SerializeField] private Slider _moodSlider;

    private float _bonusTime { get; set; }

    private float _crowdMood;


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
    }
}
