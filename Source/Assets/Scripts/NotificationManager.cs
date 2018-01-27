using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.GameState == "Transmission")
        {
            transform.Find("TransmissionMessage").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("TransmissionMessage").gameObject.SetActive(false);
        }
    }
}
