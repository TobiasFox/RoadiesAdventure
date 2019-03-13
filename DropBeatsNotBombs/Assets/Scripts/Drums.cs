using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Drums : MonoBehaviour
{
    public Image Image_Drums;
    public Button B_Input;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Image_Drums.enabled = true;
            B_Input.enabled = true;

        }
    }
}
