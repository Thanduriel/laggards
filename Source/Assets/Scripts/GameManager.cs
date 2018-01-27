using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject TextPrefab;
    public GameObject ExecutionList;
    public static string GameState = "Offline";
    public static int executionTime = 2;
	// Use this for initialization
	void Start () {
        ExecutionList = GameObject.Find("ExecutionList");
        GameState = "Planning";
}
	
	// Update is called once per frame
	void Update () {
		if(GameState == "Transmission" && ExecutionList.transform.childCount > 1)
        {
            clearExecutionList();
        }
	}

    //Clear Execution List
   void clearExecutionList()
    {
        foreach (Transform child in ExecutionList.transform)
        {
            if(child.gameObject.name != "ChainText")
            GameObject.Destroy(child.gameObject);
        }

    }
}
