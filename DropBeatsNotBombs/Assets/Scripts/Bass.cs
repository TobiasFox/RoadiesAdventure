using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bass : MonoBehaviour
{
    public Button B_Minus, B_Plus, B_Example, B_Play;
    public GameObject Image_Bass;
    public float Pitch = 0;
    public AudioSource ASource;
    private void Start()
    {
        ASource = gameObject.GetComponent<AudioSource>();
        ASource.pitch = Pitch;
    }
    public void PlayExample()
    {
        B_Example.enabled = false;
        B_Minus.enabled = false;
        B_Plus.enabled = false;
        B_Play.enabled = false;
        ASource.pitch = 1;
        ASource.Play();
        StartCoroutine(Disableonplay());
    }

    public void PlusPitch()
    {
        Pitch += 0.25f;
        ASource.pitch = Pitch;
    }
    public void MinusPitch()
    {
        if (Pitch > 0.25)
        {
            Pitch -= 0.25f;
        }
        else
        {
            Pitch = 0.25f;
        }

        ASource.pitch = Pitch;
    }
    public void PlayBass()
    {
        if (ASource.pitch != 1)
        {
            ASource.Play();
        }
        else
        {
            //TODO: Destroy Gameobject. Rätsel gelöst
            Debug.Log("Bestanden");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Image_Bass.SetActive(true);
        }
    }
    IEnumerator Disableonplay()
    {
        yield return new WaitForSeconds(3f);
        B_Example.enabled = true;
        B_Minus.enabled = true;
        B_Plus.enabled = true;
        B_Play.enabled = true;
        ASource.pitch = Pitch;

    }
}
