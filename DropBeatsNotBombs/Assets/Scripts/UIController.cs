using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private float _crowdMoodTotalTimeInSeconds = 500;
    [SerializeField] private Slider _moodSlider;
    [SerializeField] private Image _inventoryImage; 
    [Tooltip("Use the same indexes as the Instrument enum: Empty, Drums, Bass, Synthesizer1, Synthesizer2")] [SerializeField] private Sprite[] _instrumentImages;
    [SerializeField] private ParticleSystem[] _moodParticles;


    public float _bonusTime { get; set; }
    private Instrument _instrument = Instrument.Empty;
    private bool _gameover = false;

    private float _crowdMood;
    private AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        _crowdMood = 1;
        _audioManager = FindObjectOfType<AudioManager>();
        _audioManager.Play("Crowd");
    }

    // Update is called once per frame
    void Update()
    {
        _crowdMood = Mathf.Clamp((_crowdMoodTotalTimeInSeconds - Time.timeSinceLevelLoad + _bonusTime), 0, float.MaxValue) / _crowdMoodTotalTimeInSeconds;
        _moodSlider.value = _crowdMood;

        UpdateMoodParticles();
        if(_crowdMood == 0 && !_gameover)
        {
            GameOver();
        }
        
        if(_gameover && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Return)))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //only for testing:
        
        ////Adding bonus time
        //if(Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    _bonusTime += 10;
        //}
        ////change inventory
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    SetNewInstrument((Instrument)UnityEngine.Random.Range(0, _instrumentImages.Length));
        //}
    }

    private void UpdateMoodParticles()
    {
        int moodParticlesIndex = (int) Math.Round(_moodParticles.Length * _crowdMood);

        for(int i = 0; i<_moodParticles.Length; i++)
        {
            ParticleSystem particleSys = _moodParticles[i];
            if(i== moodParticlesIndex)
            {
                if(particleSys.isPlaying)
                {
                    return;
                }
                particleSys.Play();
                Debug.Log("Play particle system: " + _moodParticles.Length * _crowdMood);
            }
            else
            {
                particleSys.Stop();
            }
        }
    }

    public void SetNewInstrument(Instrument instrument)
    {
        _instrument = instrument;
        _inventoryImage.sprite = _instrumentImages[(int)instrument];
    }

    private void GameOver()
    {
        _gameover = true;
        _audioManager.Play("Boo");
        //TODO geht auch schöner! (refactoring)
        Dialog gameOverDialog = new Dialog("Game Over");
        FindObjectOfType<DialogManager>().StartDielogue(gameOverDialog);
    }
}
