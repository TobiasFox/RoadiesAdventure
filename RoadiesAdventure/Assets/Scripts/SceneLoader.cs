using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
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

        yield return null;
    }

}
