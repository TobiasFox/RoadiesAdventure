using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Drums : MonoBehaviour
{
    public GameObject Image_Drums;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Image_Drums.SetActive(true);
        }
    }
}
