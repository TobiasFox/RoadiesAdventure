using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableInputUI : MonoBehaviour
{
    [SerializeField] private float waitTime = 5f;
    [SerializeField] [TextArea] private string[] controlTexts;
    [SerializeField] Sprite[] images;

    private Text text;
    private Image image;
    private bool isShowingControls = true;
    private float interval;

    void Start()
    {
        text = GetComponentInChildren<Text>();
        image = GetComponentInChildren<Image>();
        StartCoroutine(DestroyTest());
    }

    private IEnumerator DestroyTest()
    {
        for (int i = 0; i < controlTexts.Length; i++)
        {
            text.text = controlTexts[i];
            image.sprite = images[i];
            image.SetNativeSize();
            interval = Time.time + waitTime;

            yield return new WaitUntil(() => GetInput(i));
        }
        text.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
    }

    private bool GetInput(int step)
    {
        if (Time.time < interval)
        {
            return false;
        }

        switch (step)
        {
            case 0:
                return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0;
            case 1:
                return Input.GetKey(KeyCode.Space);
            case 2:
                return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        }
        return false;
    }

}
