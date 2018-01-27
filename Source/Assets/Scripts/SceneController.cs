using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public event Action BeforeSceneUnload;
    public event Action AfterSceneLoad;

    public CanvasGroup faderCanvasgroup;
    public float fadeDuration;
    public string startingSceneName = "MainMenu";

    private bool isFading;

	// Use this for initialization
	private IEnumerator Start ()
    {
        faderCanvasgroup.alpha = 1f;

        yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName));

        StartCoroutine(Fade(0f));
	}

    public void FadeAndLoadScene (string sceneName)
    {
        if(!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneName));
        }
    }

    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        yield return StartCoroutine(Fade(1f));

        if (BeforeSceneUnload != null)
        {
            BeforeSceneUnload();
        }

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        if (AfterSceneLoad != null)
        {
            AfterSceneLoad();
        }

        yield return StartCoroutine(Fade(0));
    }
	
    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);
    }

    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;
        faderCanvasgroup.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(faderCanvasgroup.alpha - finalAlpha) / fadeDuration;

        while(!Mathf.Approximately(faderCanvasgroup.alpha, finalAlpha))
        {
            faderCanvasgroup.alpha = Mathf.MoveTowards(faderCanvasgroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        isFading = false;
        faderCanvasgroup.blocksRaycasts = false;
    }
}
