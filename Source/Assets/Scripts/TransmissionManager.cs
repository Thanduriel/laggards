using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransmissionManager : MonoBehaviour
{

    GameObject ExecutionList;
    GameObject Player;
    bool _enableTransmission = false;

    // Use this for initialization
    void Start()
    {
        ExecutionList = GameObject.Find("ExecutionList");
        //Add Button Event
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(delegate () { if (_enableTransmission) { TrasmitActions(); } });
    }

    // Update is called once per frame
    void Update()
    {
        if (ExecutionList.transform.childCount > 1)
        {
            _enableTransmission = true;
        }
        else
        {
            _enableTransmission = false;
        }

        if (_enableTransmission)
        {
            gameObject.transform.Find("Text").GetComponent<Text>().color = Color.red;
        }
        else
        {
            gameObject.transform.Find("Text").GetComponent<Text>().color = Color.gray;
        }
    }

    void TrasmitActions()
    {
        Debug.Log("Transmitting Actions");

        //Create Transmission Array
        List<string> ActionList = new List<string>();

        //Add Transmissions To List
        foreach (Transform child in ExecutionList.transform)
        {
            if (child.gameObject.GetComponent<Text>().text != "Action Chain:")
                ActionList.Add(child.gameObject.GetComponent<Text>().text);
        }

        GameManager.GameState = "Transmission";

        StartCoroutine(executeActions(ActionList));

        
    }

    IEnumerator executeActions(List<string> ActionList)
    {
        //Execute Actions
        foreach (string action in ActionList)
        {
            switch (action)
            {
                case "MoveRightCard":
                    GetPlayer().GetComponent<PlayerController>().MoveRight();
                    break;
                case "MoveLeftCard":
                    GetPlayer().GetComponent<PlayerController>().MoveLeft();
                    break;
                case "JumpCard":
                    GetPlayer().GetComponent<PlayerController>().Jump();
                    yield return new WaitForSeconds(GameManager.executionTime);
                    GetPlayer().GetComponent<PlayerController>().Fall();
                    break;
                case "EvadeCard":
                    GetPlayer().GetComponent<PlayerController>().Evade();
                    break;
            }
            //Delay Actinos 
            yield return new WaitForSeconds(GameManager.executionTime);
        }

        GameManager.GameState = "Planning";
    }

    GameObject GetPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }
}
