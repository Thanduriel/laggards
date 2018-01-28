using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditSceneTimeout : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(SceneTimeout());
	}

    private IEnumerator SceneTimeout()
    {
        yield return new WaitForSeconds(30);
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("enter"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
