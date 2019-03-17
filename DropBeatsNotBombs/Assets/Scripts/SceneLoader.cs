using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool _isDone = false;
    private AsyncOperation _asyncOperation;

    private void Start()
    {
        StartAsyncSceneLoading("JuliusScene");
    }

    public void StartAsyncSceneLoading(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    public void ChangeScene(string sceneName)
    {
        _asyncOperation.allowSceneActivation = true;
    }

    private IEnumerator LoadScene(string sceneName)
    {
        _asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        _asyncOperation.allowSceneActivation = false;

        while (!_isDone)
        {
            if (_asyncOperation.progress < .9f)
            {
                yield return null;
            }
            _isDone = true;
        }
        Debug.Log("Loading complete");
    }


}
