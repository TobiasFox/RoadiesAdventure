using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkManager : MonoBehaviour
{
    [System.NonSerialized]
    public Camera cam;

    private bool inputRequest = false;
    private List<KeyCode> inputOrder = new List<KeyCode>();
    private int currentButton = 0;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        cam.gameObject.SetActive(false);
    }

    public IEnumerator SimpleRoutine()
    {
        yield return new WaitForSeconds(.5f);

        inputOrder.Add(KeyCode.Alpha1);
        inputOrder.Add(KeyCode.Alpha2);
        inputOrder.Add(KeyCode.Alpha3);
        inputOrder.Add(KeyCode.Alpha4);

        for (int i = 0; i < transform.childCount; i++)
        {
            var blinkMat = transform.GetChild(i).GetComponent<BlinkingMaterial>();
            if (blinkMat != null)
            {
                blinkMat.StartBlinking();
                yield return new WaitForSeconds(.6f);
                blinkMat.StopBlinking();
            }
        }
        inputRequest = true;
        Debug.Log("start puzzle, wait for: " + inputOrder[currentButton]);
    }

    public void StartBlinking()
    {
        StartCoroutine("SimpleRoutine");
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    var blinkMat = transform.GetChild(i).GetComponent<BlinkingMaterial>();
        //    if (blinkMat != null)
        //    {
        //        blinkMat.StartBlinking();
        //    }
        //}
    }

    public void StopBlinking()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<BlinkingMaterial>().StopBlinking();
        }
    }

    private void Update()
    {
        if (!inputRequest)
        {
            return;
        }

        if (Input.GetKeyUp(inputOrder[currentButton]))
        {
            currentButton++;
            Debug.Log(" currentButton " + currentButton);

            if (currentButton >= inputOrder.Capacity)
            {
                Debug.Log("finish puzzle");
                inputRequest = false;
                Destroy(gameObject);
                return;
            }
            Debug.Log(" wait for next input from " + inputOrder[currentButton]);
        }
        else
        {
            if (Input.anyKey && !Input.GetKey(inputOrder[currentButton]))
            {
                currentButton = 0;
                Debug.Log("reset puzzle");
            }
        }
    }

}
