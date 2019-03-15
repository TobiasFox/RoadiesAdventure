using Invector.CharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DrumsUpdate : MonoBehaviour
{
    public float time = 0;
    public int Seconds = 0;
    public AudioSource ASource;
    public Button B_Drum;
    public bool FirstInput = false;
    public bool SecondInput = false;
    public bool ThirdInput = false;
    private bool Played1 = false;
    private bool Played2 = false;
    private bool Played3 = false;
    public Image Indicator1;
    public Image Indicator2;
    public Image Indicator3;
    public GameObject Drum;
    // Update is called once per frame


    void Update()
    {
        if(FirstInput == true)
        {
            Indicator1.color = Color.green;
        }
        else
        {
            Indicator1.color = Color.red;
        }
        if (SecondInput == true)
        {
            Indicator2.color = Color.green;
        }
        else
        {
            Indicator2.color = Color.red;
        }
        if (ThirdInput == true)
        {
            Indicator3.color = Color.green;
        }
        else
        {
            Indicator3.color = Color.red;
        }

        if (Seconds >= 4f)
        {
            time = 0.0f;
            Seconds = 0;
            FirstInput = false;
            SecondInput = false;
            ThirdInput = false;
            Played1 = false;
            Played2 = false;
            Played3 = false;
            B_Drum.onClick.RemoveAllListeners();
        }

        time += Time.deltaTime;
        Seconds = Convert.ToInt32(time % 60);
        if (Seconds == 1 && Played1 == false)
        {
            ASource.Play();
            Played1 = true;
        }
        else if (Seconds == 2 && Played2 == false)
        {
            ASource.Play();
            Played2 = true;
        }
        else if (Seconds == 3 && Played3 == false)
        {
            ASource.Play();
            Played3 = true;
        }




        if (Seconds == 1)
        {
            B_Drum.onClick.RemoveAllListeners();
            B_Drum.onClick.AddListener(SetFirstBoolOnTrue);

        }
        if (Seconds == 2)
        {
            B_Drum.onClick.RemoveAllListeners();
            B_Drum.onClick.AddListener(SetSecondBoolOnTrue);
        }
        if (Seconds == 3)
        {
            B_Drum.onClick.RemoveAllListeners();
            B_Drum.onClick.AddListener(SetThirdBoolOnTrue);

        }
        if (Seconds <= 3 && FirstInput == true && SecondInput == true && ThirdInput == true)
        {
            GameObject.FindGameObjectWithTag("Player").transform.Find("RoadiePrefab").GetComponent<vThirdPersonInput>().MouseLocked = false;
            GameObject.FindGameObjectWithTag("Player").transform.Find("RoadiePrefab").GetComponent<CollectInstrument>().AddInstrument(Instrument.Drums);
            GameObject.FindGameObjectWithTag("Player").transform.Find("RoadiePrefab").GetComponent<vThirdPersonInput>().ResetAndLockMovement(false);
            Cursor.visible = false;
            Destroy(Drum);
            gameObject.SetActive(false);
        }
    }


    void SetFirstBoolOnTrue()
    {
        if (FirstInput == false)
        {
            FirstInput = true;
        }
        else
        {
            FirstInput = false;
            SecondInput = false;
            ThirdInput = false;
        }
        Debug.Log("test");
    }
    void SetSecondBoolOnTrue()
    {
        if (SecondInput == false)
        {
            SecondInput = true;
        }
        else
        {
            FirstInput = false;
            SecondInput = false;
            ThirdInput = false;
        }
    }
    void SetThirdBoolOnTrue()
    {
        if (ThirdInput == false)
        {
            ThirdInput = true;
        }
        else
        {
            FirstInput = false;
            SecondInput = false;
            ThirdInput = false;
        }
    }
}
