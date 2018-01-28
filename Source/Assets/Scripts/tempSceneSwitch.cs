using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempSceneSwitch : MonoBehaviour {
    public SceneController sc;
    public int sceneIndex = 0;
    private string[] sceneNames = { "MainMenu", "Gameplay", "Scores" };


	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Submit")) {
            sceneIndex = (sceneIndex + 1) % sceneNames.Length;

            sc.FadeAndLoadScene(sceneNames[sceneIndex]);

        }

    }
}
