using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GifAnimation : MonoBehaviour
{
    public Sprite[] frames;
    public float framesPerSecond = 10.0f;
    Image BackgroundImage;
    // Start is called before the first frame update
    void Start()
    {
        BackgroundImage = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        int index = Convert.ToInt32(Time.time * framesPerSecond);
        index = index % frames.Length;
        BackgroundImage.sprite = frames[index];
    }
}
